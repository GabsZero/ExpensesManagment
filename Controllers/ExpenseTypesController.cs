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
    public class ExpenseTypesController : Controller
    {
        private readonly ApplicationContext _context;

        public ExpenseTypesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: ExpenseTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExpenseTypes.ToListAsync());
        }

        public async Task<JsonResult> TableData()
        {
            return Json(await _context.ExpenseTypes.ToListAsync());
        }

        // GET: ExpenseTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _context.ExpenseTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseType == null)
            {
                return NotFound();
            }

            return View(expenseType);
        }

        public async Task<JsonResult> ExpenseTypeExist(string name)
        {
            if (await _context.ExpenseTypes.AnyAsync(et => et.Name.ToLower() == name.ToLower()))
                return Json("This expense type already exists");

            return Json(true);
        }

        // GET: ExpenseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpenseTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                TempData["confirmation"] = expenseType.Name + " was successfully created!";
                _context.Add(expenseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenseType);
        }

        // GET: ExpenseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _context.ExpenseTypes.FindAsync(id);
            if (expenseType == null)
            {
                return NotFound();
            }
            return View(expenseType);
        }

        // POST: ExpenseTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ExpenseType expenseType)
        {
            if (id != expenseType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["confirmation"] = expenseType.Name + " was successfully updated!";
                    _context.Update(expenseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseTypeExists(expenseType.Id))
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
            return View(expenseType);
        }

        // POST: ExpenseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<JsonResult> Delete(int id)
        {
            var expenseType = await _context.ExpenseTypes.FindAsync(id);
            TempData["confirmation"] = expenseType.Name + " was successfully removed!";
            _context.ExpenseTypes.Remove(expenseType);
            await _context.SaveChangesAsync();
            return Json(expenseType.Name + " was successfully removed from your data");
        }

        private bool ExpenseTypeExists(int id)
        {
            return _context.ExpenseTypes.Any(e => e.Id == id);
        }
    }
}
