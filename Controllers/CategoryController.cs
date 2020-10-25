using API.NETcore2.Data;
using API.NETcore2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.NETcore2.Controllers
{
    public class CategoryController : Controller
    {

        private readonly StoreDataContext _context;

        public CategoryController(StoreDataContext context)
        {
            _context = context;
        }

        [Route("v1/categories")]
        [HttpGet]
        //[ResponseCache(Duration = 60)] //Salvar no cache, atualiza a cada 60 minutos / Cache-control: public, max-age=60 *UTILIZAR PARA DADOS QUE NÃO SÃO MUTADOS TODA HORA*
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)] //colocando o location ele salva direto na máquina do usuário
        public IEnumerable<Category> Get()
        {
                
            return _context.Categories.AsNoTracking().ToList();
        }

        [Route("v1/categories/{id}")]
        [HttpGet]
        public Category Get(int id)
        {
            return _context.Categories.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        [Route("v1/categories/{id}/products")]
        [HttpGet]
        public IEnumerable<Product> GetProducts(int id)
        {
            return _context.Products.AsNoTracking().Where(x => x.CategoryId == id).ToList();
        }

        [Route("v1/categories")]
        [HttpPost]
        public Category Post([FromBody] Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges(); //commit

            return category;
        }

        [Route("v1/categories")]
        [HttpPut]
        public Category Put([FromBody] Category category)
        {
            _context.Entry<Category>(category).State = EntityState.Modified;
            _context.SaveChanges(); //commit

            return category;
        }

        [Route("v1/categories")]
        [HttpDelete]
        public Category Delete([FromBody] Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges(); //commit

            return category;
        }
    }
}
