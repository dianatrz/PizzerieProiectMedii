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
    public class IndexModel : PageModel
    {
        private readonly AppWeb_Proiect.Data.AppWeb_ProiectContext _context;

        public IndexModel(AppWeb_Proiect.Data.AppWeb_ProiectContext context)
        {
            _context = context;
        }

        public IList<Specificatie> Specificatie { get;set; }

        public async Task OnGetAsync()
        {
            Specificatie = await _context.Specificatie.ToListAsync();
        }
    }
}
