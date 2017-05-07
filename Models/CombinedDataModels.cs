using DomowaBibliotekaCore.Models.Data_Models;
using Microsoft.AspNetCore.Http;

namespace DomowaBibliotekaCore.Models
{
    public class CombinedDataModels
    {
        public Authors Authors { get; set; }
        public Books Books { get; set; }
        public Genres Genres { get; set; }
        public Series Series { get; set; }

        public IFormFile cover { get; set; }
        public IFormFile photo { get; set; }
    }
}