using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomowaBibliotekaCore.Models.Data_Models
{
    public class Book_Genres
    {
        public long id { get; set; }

        [ForeignKey("GenreId")]
        public virtual Genres Genre { get; set; }
        public long GenreId { get; set; }
   
        [ForeignKey("BookId")]
        public virtual Books Book { get; set; }
        public long BookId { get; set; }
    }
}