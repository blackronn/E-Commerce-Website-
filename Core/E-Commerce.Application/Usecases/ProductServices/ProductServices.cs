using E_Commerce.Application.Dtos.ProductDtos;
using E_Commerce.Application.Interfaces;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Usecases.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository<Product> _productRepository;

        public ProductServices(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task CreateProductAsync(CreateProductDto model)
        {
            await _productRepository.CreateAsync(new Product
            {
                ProductName = model.ProductName,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                CategoryID = model.CategoryID,
                ImageUrl = model.ImageUrl,
            });
        }
        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            await _productRepository.DeleteAsync(product);

        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(x=> new ResultProductDto
            {
                ProductID = x.ProductID,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                CategoryID = x.CategoryID,
                ImageUrl = x.ImageUrl,
            }).ToList();

        }

        public async Task<GetByIdProductDto> GetByIdAsync(int id)
        {
            var values = await _productRepository.GetByIdAsync(id);
            return new GetByIdProductDto
            {
                ProductID = values.ProductID,
                ProductName = values.ProductName,
                Description = values.Description,
                Price = values.Price,
                Stock = values.Stock,
                CategoryID = values.CategoryID,
                ImageUrl = values.ImageUrl,
            };
        }

        public async Task UpdateProductAsync(UpdateProductDto model)
        {
            var product = await _productRepository.GetByIdAsync(model.ProductID);
            product.ProductName = model.ProductName;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Stock = model.Stock;
            product.CategoryID = model.CategoryID;
            product.ImageUrl = model.ImageUrl;
            await _productRepository.UpdateAsync(product);
        }
    }
}

