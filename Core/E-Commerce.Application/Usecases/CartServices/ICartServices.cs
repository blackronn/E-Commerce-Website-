using E_Commerce.Application.Dtos.CartDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Usecases.CartServices
{
    public interface ICartServices
    {
        Task<List<ResultCartDto>> GetAllCartAsync();
        Task<GetByIdCartDto> GetCartByIdAsync(int id);
        Task CreateCartAsync(CreateCartDto createCartDto);
        Task UpdateCartAsync(UpdateCartDto updateCartDto);
        Task DeleteCartAsync(int id);
    }
}
