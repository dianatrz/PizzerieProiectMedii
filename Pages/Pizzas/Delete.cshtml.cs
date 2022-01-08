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
    public class DeleteModel : PageModel
    {
        private readonly AppWeb_Proiect.Data.AppWeb_ProiectContext _context;

        public DeleteModel(AppWeb_Proiect.Data.AppWeb_ProiectContext context)
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

            Pizza = await _context.Pizza.FirstOrDefaultAsync(m => m.ID == id);

            if (Pizza == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pizza = await _context.Pizza.FindAsync(id);

            if (Pizza != null)
            {
                _context.Pizza.Remove(Pizza);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
