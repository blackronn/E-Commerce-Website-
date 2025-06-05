using E_Commerce.Application.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Usecases.ProductServices
{
    public interface IProductServices
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task<GetByIdProductDto> GetByIdAsync(int id);
        Task CreateProductAsync(CreateProductDto model);
        Task UpdateProductAsync(UpdateProductDto model);
        Task DeleteProductAsync(int id);
        Task<List<ResultProductDto>> GetProductsTakeAsync(int take);
    }
}
