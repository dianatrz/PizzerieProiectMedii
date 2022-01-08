using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppWeb_Proiect.Models
{
    public class Ingredient
    {
        public int ID { get; set; }

        [Display(Name = "Ingredient")]
        public string NumeIngredient { get; set; }
        public ICollection<PizzaIngredient> PizzaIngredients { get; set; }
    }
}
