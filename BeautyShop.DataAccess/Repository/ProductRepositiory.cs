using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyShop.DataAccess.Data;
using BeautyShop.DataAccess.Repository.IRepository;
using BeautyShop.Models.Models;

namespace BeautyShop.DataAccess.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private AppDbContext _context;
    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public void Update(Product product)
    {
        var productInDb=_context.Products.FirstOrDefault(p=>p.Id== product.Id);
        if(productInDb!=null)
        {
            productInDb.Name=product.Name;
            productInDb.Brand=product.Brand;
            productInDb.Description=product.Description;
            productInDb.Price=product.Price;
            productInDb.SkinType=product.SkinType;
            productInDb.CategoryId = product.CategoryId;
            if (productInDb.Picture != null)
            {
                productInDb.Picture = product.Picture;
            }
        }

    }
}
