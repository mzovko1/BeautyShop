﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyShop.DataAccess.Data;
using BeautyShop.DataAccess.Repository.IRepository;

namespace BeautyShop.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private AppDbContext _context;
    public ICategoryRepository Category { get; private set; }
    public IProductRepository Product { get; private set; }


    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Category = new CategoryRepository(_context);
        Product = new ProductRepository(_context);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
