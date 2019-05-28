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
    public class IncomeTypesController : Controller
    {
        private readonly ApplicationContext _context;

        public IncomeTypesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: IncomeTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.IncomeTypes.ToListAsync());
        }

        // GET: IncomeTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeType = await _context.IncomeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incomeType == null)
            {
                return NotFound();
            }

            return View(incomeType);
        }

        // GET: IncomeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IncomeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] IncomeType incomeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incomeType);
                await _context.SaveChangesAsync();
                TempData["confirmation"] = "Income type " + incomeType.Name + " was successfully created!";
                return RedirectToAction(nameof(Index));
            }
            return View(incomeType);
        }

        // GET: IncomeTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeType = await _context.IncomeTypes.FindAsync(id);
            if (incomeType == null)
            {
                return NotFound();
            }
            return View(incomeType);
        }

        // POST: IncomeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] IncomeType incomeType)
        {
            if (id != incomeType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incomeType);
                    await _context.SaveChangesAsync();
                    TempData["confirmation"] = "Income type " + incomeType.Name + " was successfully updated!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomeTypeExists(incomeType.Id))
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
            return View(incomeType);
        }

        // GET: IncomeTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeType = await _context.IncomeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incomeType == null)
            {
                return NotFound();
            }

            return View(incomeType);
        }

        // POST: IncomeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incomeType = await _context.IncomeTypes.FindAsync(id);
            TempData["confirmation"] = "Income type " + incomeType.Name + " was successfully deleted!";
            _context.IncomeTypes.Remove(incomeType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncomeTypeExists(int id)
        {
            return _context.IncomeTypes.Any(e => e.Id == id);
        }
    }
}
