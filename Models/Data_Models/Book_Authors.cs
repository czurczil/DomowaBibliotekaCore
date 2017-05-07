using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DomowaBibliotekaCore.Models.Data_Models
{
    public class Book_Authors
    {
        public long id { get; set; }

        //[ForeignKey("AuthorId")]
        public virtual Authors Author { get; set; }
        public long AuthorId { get; set; }

        //[ForeignKey("BookId")]
        public virtual Books Book { get; set; }
        public long BookId { get; set; }

    }
}