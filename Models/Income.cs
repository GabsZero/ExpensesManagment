using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagment.Models
{
    public class Income
    {
        public int Id { get; set; }
        public int MonthId { get; set; }
        public Month Month { get; set; }
        public int IncomeTypeId { get; set; }
        public IncomeType IncomeType { get; set; }
        [Required]
        [Range(0, float.MaxValue)]
        public decimal Amount { get; set; }
    }
}
