using BudgetTrackerDemo.Data;
using BudgetTrackerDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BudgetTrackerDemo.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly BudgetTrackerContext _context;
        public ExpensesController(BudgetTrackerContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var expenses = _context.Expenses.Include(e => e.Category).ToList();
            CalculateTotalExpenses(expenses);
            return View(expenses);
        }

        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Expense expense)
        {
            if (expense is null) return View();

            if (expense.CategoryId == Guid.Empty)
            {
                var uncategorizedCategory = _context.Categories.FirstOrDefault(c => c.Name == "Uncategorized");
                if (uncategorizedCategory != null)
                {
                    expense.CategoryId = uncategorizedCategory.Id;
                }
            }

            _context.Expenses.Add(expense);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {

            var savedExpense = _context.Expenses.FirstOrDefault(exp => exp.Id == id);
            if (savedExpense == null) return NotFound();
            ViewData["Categories"] = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View(savedExpense);
        }

        [HttpPost]
        public IActionResult Edit(Guid id, Expense expense)
        {
            if (expense is null) return View();

            var savedExpense = _context.Expenses.FirstOrDefault(exp => exp.Id == id);
            if (savedExpense is null) return View();

            savedExpense.Name = expense.Name;
            savedExpense.Amount = expense.Amount;
            savedExpense.Category = expense.Category;
            savedExpense.Date = expense.Date;

            _context.Update(savedExpense);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var savedExpense = _context.Expenses.FirstOrDefault(exp => exp.Id == id);
            if (savedExpense == null) return NotFound();

            _context.Expenses.Remove(savedExpense);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        #region private methods
        private void CalculateTotalExpenses(List<Expense> expenses)
        {
            var categoryTotals = expenses
                .GroupBy(e => e.Category != null ? e.Category.Name : "Uncategorized")
                .Select(g => new {
                    Category = g.Key,
                    Total = g.Sum(e => e.Amount),
                    Count = g.Count()
                })
                .ToList();

            var grandTotal = categoryTotals.Sum(ct => ct.Total);

            ViewBag.CategoryTotals = categoryTotals;
            ViewBag.GrandTotal = grandTotal;


        }

        #endregion private methods

    }
}
