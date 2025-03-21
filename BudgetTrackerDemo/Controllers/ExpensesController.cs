using BudgetTrackerDemo.Data;
using BudgetTrackerDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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


    }
}
