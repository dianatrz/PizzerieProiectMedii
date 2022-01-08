using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppWeb_Proiect.Models
{
    public class PizzaIngredient
    {
        public int ID { get; set; }
        public int PizzaID { get; set; }
        public Pizza Pizza { get; set; }
        public int IngredientID { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
