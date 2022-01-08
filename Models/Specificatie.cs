using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppWeb_Proiect.Models
{
    public class Specificatie
    {
        public int ID { get; set; }

        [Display(Name = "Specificatie")]
        public string NumeSpecificatie { get; set; }
        public ICollection<Pizza> Pizzas { get; set; }
    }
}
