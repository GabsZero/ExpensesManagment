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

        public async Task<JsonResult> getIncomes(int monthId)
        {
            var incomes = _context.Incomes.Include(e => e.IncomeType).Include(e => e.Month)
                            .Where(m => m.MonthId == monthId)
                            .GroupBy(e => new { e.Month, e.IncomeType })
                            .Select(e => new {
                                e.Key.Month,
                                e.Key.IncomeType,
                                Amount = e.Sum(s => s.Amount)
                            });

            var incomesDataSet = new
            {
                type = new List<string>(),
                amount = new List<float>()
            };
            foreach (var income in incomes)
            {
                incomesDataSet.type.Add(income.IncomeType.Name);
                incomesDataSet.amount.Add(income.Amount);
            };

            return Json(incomesDataSet);
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

        public async Task<JsonResult> GetMonthResult(int monthId)
        {
            var monthResult = _context.Months.Include(m => m.Expenses)
                .Where(m => m.Id == monthId).Select(m => new { incomes = m.Incomes.Sum(i => i.Amount), expenses = m.Expenses.Sum(e => e.Amount) });

            var monthResultDataset = new
            {
                expenses = monthResult.First().expenses,
                incomes = monthResult.First().incomes
            };

            return Json(monthResultDataset);
        }
    }
}