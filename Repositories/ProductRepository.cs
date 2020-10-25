﻿using API.NETcore2.Data;
using API.NETcore2.Models;
using API.NETcore2.ViewModels.ProductViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.NETcore2.Repositories
{
    public class ProductRepository
    {

        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<ListProductViewModel> Get()
        {
            return _context
                .Products
                .Include(x => x.Category)
                .Select(x => new ListProductViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    Category = x.Category.Title,
                    CategoryId = x.Category.Id
                })
                .AsNoTracking()
                .ToList();
        }

        public Product Get(int idProduct)
        {
            return _context.Products.Find(idProduct);
        }

        public void Save(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            
        }

        public void Update(Product product)
        {
            _context.Entry<Product>(product).State = EntityState.Modified;
            _context.SaveChanges();
        }
        
        public bool Delete(int id)
        {
            Product product = _context.Products.Find(id);
            if (product==null)
            {
                return false;
            }
            else
            {
                

                _context.Products.Remove(product);
                _context.SaveChanges(); //commit
                return true;

            }


        }


    }
}