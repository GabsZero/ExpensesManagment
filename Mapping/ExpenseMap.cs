using ExpenseManagment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagment.Mapping
{
    public class ExpenseMap : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasOne(e => e.Month).WithMany(e => e.Expenses);
            builder.HasOne(e => e.ExpenseType).WithMany(e => e.Expenses);
        }
    }
}
