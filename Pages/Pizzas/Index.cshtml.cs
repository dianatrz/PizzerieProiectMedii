using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppWeb_Proiect.Data;
using AppWeb_Proiect.Models;

namespace AppWeb_Proiect.Pages.Pizzas
{
    public class IndexModel : PageModel
    {
        private readonly AppWeb_Proiect.Data.AppWeb_ProiectContext _context;

        public IndexModel(AppWeb_Proiect.Data.AppWeb_ProiectContext context)
        {
            _context = context;
        }

        public IList<Pizza> Pizza { get;set; }

        public PizzaData PizzaD { get; set; }
        public int PizzaID { get; set; }
        public int IngredientID { get; set; }
        public async Task OnGetAsync(int? id, int? ingredientID)
        {
            PizzaD = new PizzaData();

            PizzaD.Pizzas = await _context.Pizza
            .Include(b => b.Specificatie)
            .Include(b => b.PizzaIngredients)
            .ThenInclude(b => b.Ingredient)
            .AsNoTracking()
            .OrderBy(b => b.Denumire)
            .ToListAsync();
            if (id != null)
            {
                PizzaID = id.Value;
                Pizza pizza = PizzaD.Pizzas
                .Where(i => i.ID == id.Value).Single();
                PizzaD.Ingredients = pizza.PizzaIngredients.Select(s => s.Ingredient);
            }
        }
    }
}
