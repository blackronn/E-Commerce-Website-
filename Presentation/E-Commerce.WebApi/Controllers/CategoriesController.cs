using E_Commerce.Application.Dtos.CategoryDtos;
using E_Commerce.Application.Usecases.CategoryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryServices _categoryServices;

        public CategoriesController(CategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _categoryServices.GetAllCategoryAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var result = await _categoryServices.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(CreateCategoryDto model)
        {
            await _categoryServices.CreateCategoryAsync(model);
            return Ok("Category Created Successfully");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategoryAsync(UpdateCategoryDto model)
        {
            await _categoryServices.UpdateCategoryAsync(model);
            return Ok("Category Updated Successfully");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            await _categoryServices.DeleteCategoryAsync(id);
            return Ok("Category Deleted Successfully");
        }
    }
}
