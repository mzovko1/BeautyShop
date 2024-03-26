using BeautyShop.Models.Models;
using BeautyShop.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace BeautyShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _context.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name.Length > 10)
            {
                ModelState.AddModelError("Name", "The name can not be longer than 10 caharacters.");
            }
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order can't be the same as the name");
            }
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["success"] = "Category created sucessfully!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Edit(int? categoryId)
        {
            Category? category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);
            return View(category);
        }
    }
}
