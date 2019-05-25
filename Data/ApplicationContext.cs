using ExpenseManagment.Mapping;
using ExpenseManagment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagment.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Month> Months { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<IncomeType> IncomeTypes { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExpenseMap());
            modelBuilder.ApplyConfiguration(new ExpenseTypeMap());
            modelBuilder.ApplyConfiguration(new IncomeMap());
            modelBuilder.ApplyConfiguration(new IncomeTypeMap());
            modelBuilder.ApplyConfiguration(new MonthMap());

        }
    }
}
