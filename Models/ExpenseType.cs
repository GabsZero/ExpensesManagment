using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagment.Models
{
    public class ExpenseType
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Remote("ExpenseTypeExist", "ExpenseTypes")]
        public string Name { get; set; }
        public ICollection<Expense> Expenses { get; set; }

    }
}
