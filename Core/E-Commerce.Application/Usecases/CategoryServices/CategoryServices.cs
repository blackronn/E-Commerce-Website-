using E_Commerce.Application.Dtos.CategoryDtos;
using E_Commerce.Application.Interfaces;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Usecases.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IRepository<Category> 
            _repository;

        public CategoryServices(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto model)
        {
            await _repository.CreateAsync(new Category
            {
                CategoryName = model.CategoryName,
            });
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(category);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(x=> new ResultCategoryDto
            {
                CategoryID = x.CategoryID,
                CategoryName = x.CategoryName,
            }).ToList();
        }

        public async Task<GetByIdCategoryDto> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            var newcategory = new GetByIdCategoryDto
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
            };
            return newcategory;
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto model)
        {
            var category = await _repository.GetByIdAsync(model.CategoryID);
            category.CategoryName = model.CategoryName;
            await _repository.UpdateAsync(category);
        }
    }
}
