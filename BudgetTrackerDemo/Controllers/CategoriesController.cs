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
        #region constants
        private const int PageSize = 5;
        #endregion constants
        public CategoriesController(BudgetTrackerContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            var totalCategories = _context.Categories.Count();

            var categories = _context.Categories
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            var totalPages = (int)Math.Ceiling((double)totalCategories / PageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            

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
