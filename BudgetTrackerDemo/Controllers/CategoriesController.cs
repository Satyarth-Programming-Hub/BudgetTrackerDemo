using BudgetTrackerDemo.Data;
using BudgetTrackerDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BudgetTrackerDemo.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly BudgetTrackerContext _context;
        public CategoriesController(BudgetTrackerContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category is null) return View();

            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var savedCategories = _context.Categories.FirstOrDefault(cat => cat.Id == id);
            if (savedCategories == null) return NotFound();
            return View(savedCategories);
        }

        [HttpPost]
        public IActionResult Edit(Guid id, Category category)
        {
            if (category is null) return View();

            var savedCategory = _context.Categories.FirstOrDefault(cat => cat.Id == id);
            if (savedCategory is null) return View();

            savedCategory.Name = category.Name;
 

            _context.Update(savedCategory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {

            var savedCategories = _context.Categories.FirstOrDefault(cat => cat.Id == id);
            if (savedCategories == null) return NotFound();


            _context.Remove(savedCategories);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
