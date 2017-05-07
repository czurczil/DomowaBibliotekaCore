using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DomowaBibliotekaCore.Models.Data_Models
{
    public class Authors
    {
        public long id { get; set; }

        [Required]
        [DisplayName("Imię")]
        public string firstName { get; set; }

        [Required]
        [DisplayName("Nazwisko")]
        public string lastName { get; set; }

        [DisplayName("Data urodzenia")]
        public DateTime? birthDate { get; set; }

        [DisplayName("Płeć")]
        public string sex { get; set; }

        [DisplayName("Miejsce urodzenia")]
        public string birthPlace { get; set; }

        [DisplayName("Biografia")]
        public string BIO { get; set; }

        [DisplayName("Zdjęcie")]
        public string photo { get; set; }

        public virtual ICollection<Book_Authors> Book_Authors { get; set; }
    }
}