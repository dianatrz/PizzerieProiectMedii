using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppWeb_Proiect.Models;

namespace AppWeb_Proiect.Data
{
    public class AppWeb_ProiectContext : DbContext
    {
        public AppWeb_ProiectContext (DbContextOptions<AppWeb_ProiectContext> options)
            : base(options)
        {
        }

        public DbSet<AppWeb_Proiect.Models.Pizza> Pizza { get; set; }

        public DbSet<AppWeb_Proiect.Models.Specificatie> Specificatie { get; set; }

        public DbSet<AppWeb_Proiect.Models.Ingredient> Ingredient { get; set; }
    }
}
