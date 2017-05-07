using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DomowaBibliotekaCore.Models.Data_Models
{
    public class Books
    {
        public long id { get; set; }

        [Required]
        [DisplayName("Tytuł")]
        public string title { get; set; }

        [DisplayName("Rok wydania")]
        public int year { get; set; }

        [DisplayName("Opis")]
        public string description { get; set; }

        [DisplayName("Okładka")]
        public string cover { get; set; }

        public virtual ICollection<Book_Authors> Book_Authors { get; set; }

        public virtual ICollection<Book_Genres> Book_Genres { get; set; }

        public virtual ICollection<Book_Series> Book_Series { get; set; }

        public virtual ICollection<User_Books> User_Books { get; set; }
    }
}