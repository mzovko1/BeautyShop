using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyShop.DataAccess.Data;
using BeautyShop.DataAccess.Repository.IRepository;
using BeautyShop.Models.Models;

namespace BeautyShop.DataAccess.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private AppDbContext _context;
    public CategoryRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public void Update(Category category)
    {
        throw new NotImplementedException();
    }
}
