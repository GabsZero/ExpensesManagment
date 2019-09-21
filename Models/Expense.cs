using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagment.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public int MonthId { get; set; }
        public Month Month { get; set; }
        public int ExpenseTypeId { get; set; }
        [DisplayName("Expense Type")]
        public ExpenseType ExpenseType { get; set; }

        [Required]
        [Range(0, float.MaxValue)]
        public float Amount { get; set; }
    }
}
