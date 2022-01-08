using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppWeb_Proiect.Data;
using AppWeb_Proiect.Models;

namespace AppWeb_Proiect.Pages.Ingrediente
{
    public class IndexModel : PageModel
    {
        private readonly AppWeb_Proiect.Data.AppWeb_ProiectContext _context;

        public IndexModel(AppWeb_Proiect.Data.AppWeb_ProiectContext context)
        {
            _context = context;
        }

        public IList<Ingredient> Ingredient { get;set; }

        public async Task OnGetAsync()
        {
            Ingredient = await _context.Ingredient.ToListAsync();
        }
    }
}
