using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using DomowaBibliotekaCore.Models;
using DomowaBibliotekaCore.Models.Data_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace DomowaBibliotekaCore.Controllers
{
    public class GenresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Genres
        public ActionResult Index()
        {
            return View(db.Genres.ToList());
        }

        // GET: Genres/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Genres genres = db.Genres.Find(id);
            if (genres == null)
            {
                return NotFound();
            }
            return View(genres);
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(long book_id,[Bind("id,genre")] Genres genres)
        {
            if (ModelState.IsValid)
            {
                long genre_id;

                if (BooksController.IsInDatabase(genres.genre,null,1) == false)
                {
                    db.Genres.Add(genres);

                    db.SaveChanges();

                    genre_id = genres.id;
                }
                else genre_id = db.Genres.Where(g => g.genre == genres.genre).Select(g => g.id).First();

                if (book_id != 0)
                {
                    Book_Genres bg = new Book_Genres()
                    {
                        BookId = book_id,
                        GenreId = genre_id
                    };

                    db.Book_Genres.Add(bg);

                    db.TrySaveChanges();
                }

                return RedirectToAction("Details", "Books", new { id = book_id });
            }

            return View(genres);
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Genres genres = db.Genres.Find(id);
            if (genres == null)
            {
                return NotFound();
            }
            return View(genres);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("id,genre")] Genres genres)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genres).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genres);
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Genres genres = db.Genres.Find(id);
            if (genres == null)
            {
                return NotFound();
            }
            return View(genres);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Genres genres = db.Genres.Find(id);
            db.Genres.Remove(genres);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteRelation(long book_id, string genre)
        {
            long id = db.Genres.Where(g => g.genre == genre).Select(g => g.id).First();
            Book_Genres bg = db.Book_Genres.Where(b => b.GenreId == id && b.BookId == book_id).First();
            db.Book_Genres.Remove(bg);
            db.TrySaveChanges();
            return RedirectToAction("Details", "Books", new { id = book_id });
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
