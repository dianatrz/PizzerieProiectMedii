using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppWeb_Proiect.Data;
using AppWeb_Proiect.Models;

namespace AppWeb_Proiect.Pages.Pizzas
{
    public class CreateModel : PizzaIngredientsPageModel
    {
        private readonly AppWeb_Proiect.Data.AppWeb_ProiectContext _context;

        public CreateModel(AppWeb_Proiect.Data.AppWeb_ProiectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["SpecificatieID"] = new SelectList(_context.Set<Specificatie>(), "ID", "NumeSpecificatie");

            var pizza = new Pizza();
            pizza.PizzaIngredients = new List<PizzaIngredient>();
            PopulateAssignedIngredientData(_context, pizza);

            return Page();
        }

        [BindProperty]
        public Pizza Pizza { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedIngredients)
        {
            var newPizza = new Pizza();
            if (selectedIngredients != null)
            {
                newPizza.PizzaIngredients = new List<PizzaIngredient>();
                foreach (var cat in selectedIngredients)
                {
                    var catToAdd = new PizzaIngredient
                    {
                        IngredientID = int.Parse(cat)
                    };
                    newPizza.PizzaIngredients.Add(catToAdd);
                }
            }
            if (await TryUpdateModelAsync<Pizza>(
            newPizza,
            "Pizza",
            i => i.Denumire, i => i.Pret,
            i => i.Gramaj, i => i.SpecificatieID))
            {
                _context.Pizza.Add(newPizza);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedIngredientData(_context, newPizza);
            return Page();
        }
    }
}
