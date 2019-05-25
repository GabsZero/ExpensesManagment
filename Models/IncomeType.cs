using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagment.Models
{
    public class IncomeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Income> Incomes { get; set; }


    }
}
