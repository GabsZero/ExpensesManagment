using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseManagment.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagment.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationContext _context;
        public DashboardController(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<JsonResult> GetExpenses(int monthId)
        {

            var expenses = _context.Expenses.Include(e => e.ExpenseType).Include(e => e.Month)
                            .Where(m => m.MonthId == monthId)
                            .GroupBy(e => new { e.Month, e.ExpenseType })
                            .Select(e => new {
                                e.Key.Month,
                                e.Key.ExpenseType,
                                Amount = e.Sum(s => s.Amount)
                            });

            var expensesDataSet = new {
                type = new List<string>(),
                amount = new List<float>()
            };
            foreach (var expense in expenses)
            {
                expensesDataSet.type.Add(expense.ExpenseType.Name);
                expensesDataSet.amount.Add(expense.Amount);
            };

            return Json(expensesDataSet);
        }
    }
}