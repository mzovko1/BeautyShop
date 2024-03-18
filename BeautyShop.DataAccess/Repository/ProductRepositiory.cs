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
        _context.Update(product);
    }
}
