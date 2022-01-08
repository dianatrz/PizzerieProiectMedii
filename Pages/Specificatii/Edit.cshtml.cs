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

namespace AppWeb_Proiect.Pages.Specificatii
{
    public class EditModel : PageModel
    {
        private readonly AppWeb_Proiect.Data.AppWeb_ProiectContext _context;

        public EditModel(AppWeb_Proiect.Data.AppWeb_ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Specificatie Specificatie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Specificatie = await _context.Specificatie.FirstOrDefaultAsync(m => m.ID == id);

            if (Specificatie == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Specificatie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecificatieExists(Specificatie.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SpecificatieExists(int id)
        {
            return _context.Specificatie.Any(e => e.ID == id);
        }
    }
}
