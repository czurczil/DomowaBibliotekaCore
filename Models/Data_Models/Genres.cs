using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DomowaBibliotekaCore.Models.Data_Models
{
    public class Genres
    {
        public long id { get; set; }

        [Required]
        [DisplayName("Gatunek")]
        public string genre { get; set; }

        public virtual ICollection<Book_Genres> Book_Genres { get; set; }
    }
}