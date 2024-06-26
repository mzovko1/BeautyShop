using BeautyShop.DataAccess.Repository;
using BeautyShop.DataAccess.Repository.IRepository;
using BeautyShop.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BeautyShop.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(productList);
        }

        public IActionResult Details(int? productId)
        {
            Product product = _unitOfWork.Product.Get(p => p.Id == productId, includeProperties: "Category");
            return View(product);
        }
    }
}