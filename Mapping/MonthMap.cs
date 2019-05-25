using ExpenseManagment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagment.Mapping
{
    public class MonthMap : IEntityTypeConfiguration<Month>
    {
        public void Configure(EntityTypeBuilder<Month> builder)
        {
            // when a month gets deleted, all itens inside it must be deleted too
            builder.HasMany(m => m.Expenses).WithOne(m => m.Month).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(m => m.Incomes).WithOne(m => m.Month).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
