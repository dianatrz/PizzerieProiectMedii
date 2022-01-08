using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppWeb_Proiect.Data;
using AppWeb_Proiect.Models;

namespace AppWeb_Proiect.Pages.Pizzas
{
    public class EditModel : PizzaIngredientsPageModel
    {
        private readonly AppWeb_Proiect.Data.AppWeb_ProiectContext _context;

        public EditModel(AppWeb_Proiect.Data.AppWeb_ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pizza Pizza { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pizza = await _context.Pizza
             .Include(b => b.Specificatie)
             .Include(b => b.PizzaIngredients).ThenInclude(b => b.Ingredient)
             .AsNoTracking()
             .FirstOrDefaultAsync(m => m.ID == id);


            if (Pizza == null)
            {
                return NotFound();
            }

            PopulateAssignedIngredientData(_context, Pizza);


            ViewData["SpecificatieID"] = new SelectList(_context.Set<Specificatie>(), "ID", "NumeSpecificatie");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedIngredients)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pizzaToUpdate = await _context.Pizza
            .Include(i => i.Specificatie)
            .Include(i => i.PizzaIngredients)
            .ThenInclude(i => i.Ingredient)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (pizzaToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Pizza>(
            pizzaToUpdate,
            "Pizza",
            i => i.Denumire, i => i.Pret,
            i => i.Gramaj, i => i.SpecificatieID))
            {
                UpdatePizzaIngredients(_context, selectedIngredients, pizzaToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Apelam UpdateBookCategories pentru a aplica informatiile din checkboxuri la entitatea Books care
            //este editata
            UpdatePizzaIngredients(_context, selectedIngredients, pizzaToUpdate);
            PopulateAssignedIngredientData(_context, pizzaToUpdate);
            return Page();
        }
    }
}
