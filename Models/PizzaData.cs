using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppWeb_Proiect.Models
{
    public class PizzaData
    {
        public IEnumerable<Pizza> Pizzas { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public IEnumerable<PizzaIngredient> PizzaIngredients { get; set; }
    }
}
