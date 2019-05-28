using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseManagment.Data;
using ExpenseManagment.Models;

namespace ExpenseManagment.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ApplicationContext _context;

        public ExpensesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Expenses.Include(e => e.ExpenseType).Include(e => e.Month);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.ExpenseType)
                .Include(e => e.Month)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "Id", "Name");
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MonthId,ExpenseTypeId,Amount")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expense);
                await _context.SaveChangesAsync();
                expense.ExpenseType = _context.ExpenseTypes.Find(expense.ExpenseTypeId);
                TempData["confirmation"] = "New Expense of type " + expense.ExpenseType.Name + " was successfully created!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "Id", "Name", expense.ExpenseTypeId);
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", expense.MonthId);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "Id", "Name", expense.ExpenseTypeId);
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", expense.MonthId);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MonthId,ExpenseTypeId,Amount")] Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                    expense.ExpenseType = _context.ExpenseTypes.Find(expense.ExpenseTypeId);
                    TempData["confirmation"] = "Expense of type " + expense.ExpenseType.Name + " was successfully updated!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "Id", "Name", expense.ExpenseTypeId);
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", expense.MonthId);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.ExpenseType)
                .Include(e => e.Month)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            expense.ExpenseType = _context.ExpenseTypes.Find(expense.ExpenseTypeId);
            TempData["confirmation"] = "Expense of type " + expense.ExpenseType.Name + " was successfully deleted!";
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
