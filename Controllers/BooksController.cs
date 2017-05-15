using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DomowaBibliotekaCore.Models;
using DomowaBibliotekaCore.Models.Data_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DomowaBibliotekaCore.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        List<string> sorting = new List<string>() { "Tytuł: alfabetycznie", "Tytuł: niealfabatycznie", "Rok wydania: rosnąco", "Rok wydania: malejąco" };

        // GET: Books
        public ActionResult Index(string searchTitle, string searchAuthor, string BookGenre, string Sort)
        {
            string user_id = null;
            if (User.Identity.IsAuthenticated)
            {
                user_id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            IQueryable<Books> books = from m in db.Books select m;

            GetGenreList();

            if (!String.IsNullOrEmpty(searchTitle))
                books = FindByTitle(books, searchTitle);

            if (!String.IsNullOrEmpty(searchAuthor))
                books = FindByAuthor(books, searchAuthor);

            if (!String.IsNullOrEmpty(BookGenre))
                books = FindByGenre(books, BookGenre);

            if (!String.IsNullOrEmpty(Sort))
                books = GetSortingList(books, Sort);

            ViewBag.Sort = new SelectList(sorting);

            return View(books);
        }

        // GET: Books/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return NotFound();
            }

            var viewModel = new BookDetailsModel();

            viewModel.Book = new Books
            {
                id = books.id,
                title = books.title,
                year = books.year,
                description = books.description,
                cover = books.cover
            };

            var details = RelatedData(id);

            viewModel.Details = details;

            var user_books = db.User_Books.ToList();

            string user_id = null;
            if (User.Identity.IsAuthenticated)
            {
                user_id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                foreach (User_Books ub in user_books)
                    if (user_id == ub.UserId && id == ub.BookId)
                    {
                        viewModel.isFavorite = ub.isFavorite;
                        viewModel.isOnWishList = ub.isOnWishList;
                        viewModel.isRead = ub.isRead;
                        viewModel.rating = ub.rating;
                        viewModel.comment = ub.comment;
                    }
            }


            return View(viewModel);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CombinedDataModels newBook)
        {
            if (ModelState.IsValid)
            {
                long book_id;
                long author_id;
                long genre_id;
                long series_id;                
                //***************************check if title, author, genre and series are in database************************
                    if (IsInDatabase(newBook.Books.title, null, 0) == false)
                    {
                        Books book;
                        if (newBook.cover == null)
                            book = new Books()
                            {
                                title = newBook.Books.title,
                                year = newBook.Books.year,
                                description = newBook.Books.description,
                                cover = null
                            };
                        else
                        {
                            book = new Books()
                            {
                                title = newBook.Books.title,
                                year = newBook.Books.year,
                                description = newBook.Books.description,
                                cover = newBook.cover.FileName
                            };

                            //newBook.cover.SaveAs(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["bookCovers"]) + book.cover);
                        }

                        db.Books.Add(book);

                        db.TrySaveChanges();

                        book_id = book.id;
                    }
                    else book_id = db.Books.Where(b => b.title == newBook.Books.title).Select(b => b.id).First();

                    if (IsInDatabase(newBook.Authors.firstName, newBook.Authors.lastName, 3) == false)
                    {
                        Authors author;
                        if (newBook.photo == null)
                            author = new Authors()
                            {
                                firstName = newBook.Authors.firstName,
                                lastName = newBook.Authors.lastName,
                                birthDate = newBook.Authors.birthDate,
                                birthPlace = newBook.Authors.birthPlace,
                                BIO = newBook.Authors.BIO,
                                photo = null,
                                sex = newBook.Authors.sex
                            };
                        else
                        {
                            author = new Authors()
                            {
                                firstName = newBook.Authors.firstName,
                                lastName = newBook.Authors.lastName,
                                birthDate = newBook.Authors.birthDate,
                                birthPlace = newBook.Authors.birthPlace,
                                BIO = newBook.Authors.BIO,
                                photo = newBook.photo.FileName,
                                sex = newBook.Authors.sex
                            };

                            //newBook.photo.SaveAs(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["authorPhotos"]) + author.photo);
                        }

                        db.Authors.Add(author);

                        db.TrySaveChanges();

                        author_id = author.id;
                    }
                    else author_id = db.Authors.Where(a => a.firstName == newBook.Authors.firstName && a.lastName == newBook.Authors.lastName).Select(a => a.id).First();

                    if (BookIsBounded(book_id, author_id, 0) == false)
                    {
                        Book_Authors bk = new Book_Authors()
                        {
                            BookId = book_id,
                            AuthorId = author_id
                        };

                        db.Book_Authors.Add(bk);
                    }


                    if (IsInDatabase(newBook.Genres.genre, null, 1) == false)
                    {
                        Genres genre = new Genres()
                        {
                            genre = newBook.Genres.genre
                        };

                        db.Genres.Add(genre);

                        db.TrySaveChanges();

                        genre_id = genre.id;
                    }
                    else genre_id = db.Genres.Where(g => g.genre == newBook.Genres.genre).Select(g => g.id).First();

                    if (IsInDatabase(newBook.Series.series, null, 2) == false)
                    {
                        Series series = new Series()
                        {
                            series = newBook.Series.series
                        };

                        db.Series.Add(series);

                        db.TrySaveChanges();

                        series_id = series.id;
                    }
                    else series_id = db.Series.Where(s => s.series == newBook.Series.series).Select(s => s.id).First();

                    if (BookIsBounded(book_id, genre_id, 1) == false)
                    {
                        Book_Genres bg = new Book_Genres()
                        {
                            BookId = book_id,
                            GenreId = genre_id
                        };

                        db.Book_Genres.Add(bg);
                    }

                    if (BookIsBounded(book_id, series_id, 2) == false)
                    {
                        Book_Series bs = new Book_Series()
                        {
                            BookId = book_id,
                            SeriesId = series_id
                        };

                        db.Book_Series.Add(bs);
                    }

                    db.TrySaveChanges();

                    return RedirectToAction("Index", "Books");           
            }
            return View(newBook);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return NotFound();
            }

            var editModel = new BookDetailsModel();

            editModel.Book = new Books
            {
                id = books.id,
                title = books.title,
                year = books.year,
                description = books.description,
                cover = books.cover
            };

            var details = RelatedData(id);

            var user_books = db.User_Books.ToList();

            int rate = 0;

            string user_id = null;
            if (User.Identity.IsAuthenticated)
            {
                user_id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                foreach (User_Books ub in user_books)
                    if (user_id == ub.UserId && id == ub.BookId)
                    {
                        editModel.isFavorite = ub.isFavorite;
                        editModel.isOnWishList = ub.isOnWishList;
                        editModel.isRead = ub.isRead;
                        editModel.rating = ub.rating;
                        editModel.comment = ub.comment;

                        rate = ub.rating;
                    }
            }

            var RatingList = new List<int>();
            for (int i = 0; i < 11; i++)
                RatingList.Add(i);
            ViewBag.Rating = new SelectList(RatingList, null, null, rate);

            editModel.Details = details;

            return View(editModel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookDetailsModel editedBook)
        {
            if (ModelState.IsValid)
            {
                var books = db.Books.Find(editedBook.Book.id);

                books.title = editedBook.Book.title;
                books.year = editedBook.Book.year;
                books.description = editedBook.Book.description;


                if(editedBook.cover != null)
                {
                    books.cover = editedBook.cover.FileName;
                    //editedBook.cover.SaveAs(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["bookCovers"]) + books.cover);
                }

                string user_id = null;

                if (User.Identity.IsAuthenticated)
                {
                    user_id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                    foreach (var item in db.User_Books)
                    {
                        if (item.BookId == editedBook.Book.id && item.UserId == user_id)
                        {
                            db.Entry(item).State = EntityState.Deleted;
                        }
                    }

                    var user_books = new User_Books()
                    {
                        UserId = user_id,
                        BookId = editedBook.Book.id,
                        isFavorite = editedBook.isFavorite,
                        isOnWishList = editedBook.isOnWishList,
                        isRead = editedBook.isRead,
                        rating = editedBook.rating,
                        comment = editedBook.comment
                    };

                    db.User_Books.Add(user_books);
                }

                db.Entry(books).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editedBook);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return NotFound();
            }
            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Books books = db.Books.Find(id);
            db.Books.Remove(books);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Favorite(string searchTitle, string searchAuthor, string BookGenre, string Sort)
        {
            string user_id = null;
            if (User.Identity.IsAuthenticated)
            {
                user_id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var books = from b in db.Books
                    join ub in db.User_Books on b.id equals ub.BookId
                    where ub.isFavorite == true & ub.UserId == user_id
                    select b;

            GetGenreList();

            if (!String.IsNullOrEmpty(searchTitle))
                books = FindByTitle(books, searchTitle);

            if (!String.IsNullOrEmpty(searchAuthor))
                books = FindByAuthor(books, searchAuthor);

            if (!String.IsNullOrEmpty(BookGenre))
                books = FindByGenre(books, BookGenre);

            if (!String.IsNullOrEmpty(Sort))
                books = GetSortingList(books, Sort);

            ViewBag.Sort = new SelectList(sorting);

            return View(books);
        }

        public ActionResult OnWishList(string searchTitle, string searchAuthor, string BookGenre, string Sort)
        {
            string user_id = null;
            if (User.Identity.IsAuthenticated)
            {
                user_id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var books = from b in db.Books
                        join ub in db.User_Books on b.id equals ub.BookId
                        where ub.isOnWishList == true & ub.UserId == user_id
                        select b;

            GetGenreList();

            if (!String.IsNullOrEmpty(searchTitle))
                books = FindByTitle(books, searchTitle);

            if (!String.IsNullOrEmpty(searchAuthor))
                books = FindByAuthor(books, searchAuthor);

            if (!String.IsNullOrEmpty(BookGenre))
                books = FindByGenre(books, BookGenre);

            if (!String.IsNullOrEmpty(Sort))
                books = GetSortingList(books, Sort);

            ViewBag.Sort = new SelectList(sorting);

            return View(books);
        }

        public ActionResult Read(string searchTitle, string searchAuthor, string BookGenre, string Sort)
        {
            string user_id = null;
            if (User.Identity.IsAuthenticated)
            {
                user_id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var books = from b in db.Books
                        join ub in db.User_Books on b.id equals ub.BookId
                        where ub.isRead && ub.UserId == user_id
                        select b;

            GetGenreList();

            if (!String.IsNullOrEmpty(searchTitle))
                books = FindByTitle(books, searchTitle);

            if (!String.IsNullOrEmpty(searchAuthor))
                books = FindByAuthor(books, searchAuthor);

            if (!String.IsNullOrEmpty(BookGenre))
                books = FindByGenre(books, BookGenre);

            if (!String.IsNullOrEmpty(Sort))
                books = GetSortingList(books, Sort);

            ViewBag.Sort = new SelectList(sorting);

            return View(books);
        }

        public bool IsInDatabase(string data, string data2, int n)
        {
            using (var context = new Models.ApplicationDbContext())
            {
                if (n == 0)
                {
                   if(context.Books.Any(b => b.title == data))
                        return true;
                    else return false;
                }
                else if (n == 1)
                {
                    if(context.Genres.Any(g => g.genre == data))
                        return true;
                    else return false;
                }
                else if (n == 2)
                {
                    if(context.Series.Any(s => s.series == data))
                        return true;
                    else return false;
                }
                else
                {
                    if(context.Authors.Any(a => a.firstName == data && a.lastName == data2))
                        return true;
                    else return false;
                }
            }
        }

        public bool BookIsBounded(long book_id, long second_id, int n)
        {
            using (var context = new Models.ApplicationDbContext())
            {
                if (n == 0)
                {
                    if (context.Book_Authors.Any(ba => ba.BookId == book_id && ba.AuthorId == second_id))
                        return true;
                    else return false;
                }
                else if (n == 1)
                {
                    if (context.Book_Genres.Any(bg => bg.BookId == book_id && bg.GenreId == second_id))
                        return true;
                    else return false;
                }
                else
                {
                    if (context.Book_Series.Any(bs => bs.BookId == book_id && bs.SeriesId == second_id))
                        return true;
                    else return false;
                }
            }

        }

        public List<BookDetailsList> RelatedData (long? id)
        {
            var details = new List<BookDetailsList>();

            var author = db.Authors.ToList();
            var book_authors = db.Book_Authors.ToList();

            foreach (Authors a in author)
                    foreach (Book_Authors ba in book_authors)
                        if (id == ba.BookId && a.id == ba.AuthorId)
                                details.Add(new BookDetailsList { Author = new Authors { id = a.id, firstName = a.firstName, lastName = a.lastName }, AuthorChecked = true });

            var genres = db.Genres.ToList();
            var book_genres = db.Book_Genres.ToList();

            foreach (Genres g in genres)
                    foreach (Book_Genres bg in book_genres)
                        if (g.id == bg.GenreId && id == bg.BookId)
                                details.Add(new BookDetailsList { Genre = new Genres { genre = g.genre }, GenreChecked = true });

            var series = db.Series.ToList();
            var book_series = db.Book_Series.ToList();

            foreach (Series s in series)
                    foreach (Book_Series bs in book_series)
                        if (s.id == bs.SeriesId && id == bs.BookId)
                                details.Add(new BookDetailsList { Series = new Series { series = s.series }, SeriesChecked = true });

            return details;
        }

        public void GetGenreList()
        {
            var GenreLst = new List<string>();
            var all_genres = from g in db.Genres select g.genre;
            GenreLst.AddRange(all_genres);
            ViewBag.BookGenre = new SelectList(GenreLst);
        }

        public IQueryable<Books> GetSortingList(IQueryable<Books> books, string Sort)
        {
                switch (Sort)
                {
                    case "Tytuł: alfabetycznie":
                        books = books.OrderBy(b => b.title);
                        break;
                    case "Tytuł: niealfabatycznie":
                        books = books.OrderByDescending(b => b.title);
                        break;
                    case "Rok wydania: rosnąco":
                        books = books.OrderBy(b => b.year);
                        break;
                    case "Rok wydania: malejąco":
                        books = books.OrderByDescending(b => b.year);
                        break;
                    default:
                        break;
                }

            return books;
        }

        public IQueryable<Books> FindByTitle(IQueryable<Books> books, string searchTitle)
        {
                books = books.Where(s => s.title.Contains(searchTitle));

            return books;
        }

        public IQueryable<Books> FindByAuthor(IQueryable<Books> books, string searchAuthor)
        {
            books = from b in books
                    join ba in db.Book_Authors on b.id equals ba.BookId
                    join a in db.Authors on ba.AuthorId equals a.id
                    join ub in db.User_Books on b.id equals ub.BookId
                    where (a.firstName == searchAuthor || a.lastName == searchAuthor || a.firstName + " " + a.lastName == searchAuthor || a.lastName + " " + a.firstName == searchAuthor)
                    select b;

            return books;
        }

        public IQueryable<Books> FindByGenre(IQueryable<Books> books, string BookGenre)
        {

                books = from b in books
                        join bg in db.Book_Genres on b.id equals bg.BookId
                        join g in db.Genres on bg.GenreId equals g.id
                        where g.genre == BookGenre
                        select b;

            return books;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}