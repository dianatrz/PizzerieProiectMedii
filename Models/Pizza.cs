using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppWeb_Proiect.Models
{
    public class Pizza
    {
        public int ID { get; set; }

        [Required, StringLength(150, MinimumLength = 3, ErrorMessage = "Denumirea pizzei trebuie sa aiba minim 3 litere!")]
        [Display(Name = "Nume pizza")]
        public string Denumire { get; set; }

        [Range(10, 100, ErrorMessage = "Pretul trebuie sa fie intre 10 si 100 de lei!")]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Pret { get; set; }

        [Range(200, 1000, ErrorMessage = "Gramajul trebuie sa fie intre 200g si 1000g!")]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Gramaj { get; set; }

        public int SpecificatieID { get; set; }
        public Specificatie Specificatie { get; set; }

        [Display(Name = "Ingrediente")]
        public ICollection<PizzaIngredient> PizzaIngredients { get; set; }
    }
}
