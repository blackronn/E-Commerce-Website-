using E_Commerce.Application.Dtos.CartItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Usecases.CartItemServices
{
    public interface ICartItemServices
    {
        Task<List<ResultCartItemDto>> GetAllCartItemAsync();
        Task<GetByIdCartItemDto> GetCartItemByIdAsync(int id);
        Task CreateCartItemAsync(CreateCartItemDto createCartItemDto);
        Task UpdateCartItemAsync(UpdateCartItemDto updateCartItemDto);
        Task DeleteCartItemAsync(int id);
        Task<List<ResultCartItemDto>> GetByCartIdCartItemsAsync(int cartId);
    }
}
