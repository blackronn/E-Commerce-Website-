using E_Commerce.Application.Dtos.OrderDtos;
using E_Commerce.Application.Dtos.OrderItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Usecases.OrderItemServices
{
    public interface IOrderItemServices
    {
        Task<List<ResultOrderItemDto>> GetAllOrderItemAsync();
        Task<GetByIdOrderItemDto> GetByIdAsync(int id);
        Task CreateOrderItemAsync(CreateOrderItemDto model);
        Task UpdateOrderItemAsync(UpdateOrderItemDto model);
        Task DeleteOrderItemAsync(int id);
    }
}
