using ExpenseManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagment.ViewModels
{
    public class DashboardViewModel
    {
        public Month Month { get; set; }
        public Income Income { get; set; }
    }
}
