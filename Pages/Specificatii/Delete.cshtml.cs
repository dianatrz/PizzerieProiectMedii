using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppWeb_Proiect.Data;
using AppWeb_Proiect.Models;

namespace AppWeb_Proiect.Pages.Specificatii
{
    public class DeleteModel : PageModel
    {
        private readonly AppWeb_Proiect.Data.AppWeb_ProiectContext _context;

        public DeleteModel(AppWeb_Proiect.Data.AppWeb_ProiectContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Specificatie = await _context.Specificatie.FindAsync(id);

            if (Specificatie != null)
            {
                _context.Specificatie.Remove(Specificatie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
