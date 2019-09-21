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
    public class IncomesController : Controller
    {
        private readonly ApplicationContext _context;

        public IncomesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Incomes
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Incomes.Include(i => i.IncomeType).Include(i => i.Month);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Incomes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var income = await _context.Incomes
                .Include(i => i.IncomeType)
                .Include(i => i.Month)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (income == null)
            {
                return NotFound();
            }

            return View(income);
        }

        // GET: Incomes/Create
        public IActionResult Create()
        {
            ViewData["IncomeTypeId"] = new SelectList(_context.IncomeTypes, "Id", "Name");
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name");
            return View();
        }

        // POST: Incomes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MonthId,IncomeTypeId,Amount")] Income income)
        {
            if (ModelState.IsValid)
            {
                _context.Add(income);
                await _context.SaveChangesAsync();
                income.IncomeType = _context.IncomeTypes.Find(income.IncomeTypeId);
                TempData["confirmation"] = "New income of type " + income.IncomeType.Name + " was successfully created!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["IncomeTypeId"] = new SelectList(_context.IncomeTypes, "Id", "Name", income.IncomeTypeId);
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", income.MonthId);
            return View(income);
        }

        // GET: Incomes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var income = await _context.Incomes.FindAsync(id);
            if (income == null)
            {
                return NotFound();
            }
            ViewData["IncomeTypeId"] = new SelectList(_context.IncomeTypes, "Id", "Name", income.IncomeTypeId);
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", income.MonthId);
            return View(income);
        }

        // POST: Incomes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MonthId,IncomeTypeId,Amount")] Income income)
        {
            if (id != income.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(income);
                    await _context.SaveChangesAsync();
                    TempData["confirmation"] = "Income["+ income.Id + "] was successfully updated!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomeExists(income.Id))
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
            ViewData["IncomeTypeId"] = new SelectList(_context.IncomeTypes, "Id", "Name", income.IncomeTypeId);
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", income.MonthId);
            return View(income);
        }

        // GET: Incomes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var income = await _context.Incomes
                .Include(i => i.IncomeType)
                .Include(i => i.Month)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (income == null)
            {
                return NotFound();
            }

            return View(income);
        }

        // POST: Incomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var income = await _context.Incomes.FindAsync(id);
            _context.Incomes.Remove(income);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncomeExists(int id)
        {
            return _context.Incomes.Any(e => e.Id == id);
        }
    }
}
