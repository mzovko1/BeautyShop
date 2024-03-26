﻿using BeautyShop.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using BeautyShop.Models.Models;

namespace BeautyShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> categoryList=_context.Categories.ToList();
            List<Product> productsList = _context.Products.ToList();
            return View(productsList);

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                TempData["success"] = "Product created sucessfully!";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
        public IActionResult Edit(int? productId)
        {
            if (productId == null || productId == 0)
            {
                return NotFound();
            }
            Product? product = _context.Products.FirstOrDefault(c => c.Id == productId);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                TempData["success"] = "Product edited sucessfully!";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
        public IActionResult Delete(int? productId)
        {
            if (productId == null || productId == 0)
            {
                return NotFound();
            }
            Product? product = _context.Products.FirstOrDefault(c => c.Id == productId);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? productId)
        {
            Product? product = _context.Products.FirstOrDefault(c => c.Id == productId);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            TempData["success"] = "Product deleted sucessfully!";
            return RedirectToAction("Index", "Product");

        }
    }
}