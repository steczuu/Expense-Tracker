using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MauiApp1.Models
{
    public class ExpenseModelContext : DbContext
    {
        public DbSet<ExpenseModel> Expenses { get; set; }

        public ExpenseModelContext()
        {
            this.Database.EnsureCreated();  
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = expenses_DB.db");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
