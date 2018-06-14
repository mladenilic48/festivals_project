using System.ComponentModel.DataAnnotations;

namespace FinalniTest.Models
{
    public class Place
    {
        public int Id { get; set; }

        [Required]
        public string Naziv { get; set; }

        [Range(0, 99999)]
        public int PostanskiBroj { get; set; }

    }
}