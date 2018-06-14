using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalniTest.Models
{
    public class Festival
    {
        public int Id { get; set; }

        [Required]
        public string Naziv { get; set; }
        public decimal CenaKarte { get; set; }

        [Range(0, 2018)]
        public int GodinaPrvogOdrzavanja { get; set; }

        public int MestoId { get; set; }
        public Place Mesto { get; set; }

    }
}