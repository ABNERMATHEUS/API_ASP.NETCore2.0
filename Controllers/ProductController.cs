using API.NETcore2.Data;
using API.NETcore2.Models;
using API.NETcore2.Repositories;
using API.NETcore2.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.NETcore2.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository;

        public ProductController(ProductRepository context)
        {
            _repository = context;
        }

        [Route("v1/product")]
        [HttpGet]
        public IEnumerable<ListProductViewModel> Get()
        {
            return _repository.Get();
        }

        [Route("v1/product/{id}")]
        [HttpGet]
        public Product Get(int id)
        {
            return _repository.Get(id);

        }

        [Route("v1/products")]
        [HttpPost]
        public ResultViewModel Post([FromBody] EditorProductViewModel model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel
                {
                    Sucess = false,
                    Message = "Não foi possível alterar o produto",
                    Data = model.Notifications
                };
            }
            Product product = new Product
            {
                Title = model.Title,
                CategoryId = model.CategoryId,
                CreateDate = DateTime.Now,
                Description = model.Description,
                Image = model.Image,
                LastUpdateDate = DateTime.Now,
                Price = model.Price,
                Quantity = model.Quantity

            };

            _repository.Save(product);
            

            return new ResultViewModel
            {
                Sucess = true,
                Message = "Produto cadastrado com sucesso!",
                Data = product
            };
        }

        [Route("v1/product")]
        [HttpPut]
        public ResultViewModel Put([FromBody] EditorProductViewModel model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel
                {
                    Sucess = false,
                    Message = "Não foi possível alterar o produto",
                    Data = model.Notifications
                };
            }

            Product product = _repository.Get(model.Id);
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;



            _repository.Update(product);

            return new ResultViewModel
            {
                Sucess = true,
                Message = "Produto alterado com sucesso!",
                Data = product
            };
        }

        [Route("v1/product/{id}")]
        [HttpDelete]
        public ResultViewModel Delete(int id)
        {
            bool _ok = _repository.Delete(id);
            if (_ok)
            {
                return new ResultViewModel
                {
                    Sucess = true,
                    Message = "Produto deletado com sucesso!",
                };
            }
            else
            {
                return new ResultViewModel
                {
                    Sucess = false,
                    Message = "Produto não encontrado!",
                };
            }
            }
        }
    }

