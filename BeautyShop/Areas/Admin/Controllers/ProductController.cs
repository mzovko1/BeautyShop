using BeautyShop.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using BeautyShop.Models.Models;
using BeautyShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeautyShop.Models.ViewModels;

namespace BeautyShop.Areas.Admin.Controllers;

public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;


    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }
    public IActionResult Index()
    {
        List<Product> productList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
        return View(productList);
    }

    public IActionResult Upsert(int? id)
    {
        IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString(),
        });
        ProductViewModel productViewModel = new ProductViewModel()
        {
            CategoryList = categoryList,
            Product = new Product()
        };

        if (id == null || id == 0)
        {
            //Create
            return View(productViewModel);
        }
        else
        {
            //Edit
            productViewModel.Product = _unitOfWork.Product.Get(p => p.Id == id);
            return View(productViewModel);
        }

    }
    [HttpPost]
    public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            TempData["success"] = "Product created succesfully";
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                if (!string.IsNullOrEmpty(productViewModel.Product.Picture))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, productViewModel.Product.Picture.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                productViewModel.Product.Picture = @"images\product\" + fileName;
            }

            if (productViewModel.Product.Id == 0)
            {
                _unitOfWork.Product.Add(productViewModel.Product);
            }
            else
            {
                _unitOfWork.Product.Update(productViewModel.Product);
            }

            _unitOfWork.Save();
            //TempData["success"] = "Product created sucessfully!";
            return RedirectToAction("Index", "Product");
        }
        else
        {
            productViewModel.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            });
        }
        return View();
    }

    #region API Calls
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return Json(new { data = productList });
    }
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var product = _unitOfWork.Product.Get(p => p.Id == id);
        if (product == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }
        else
        {
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.Picture.Trim('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Delete(product);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted successfully" });
        }
    }
    #endregion
}
