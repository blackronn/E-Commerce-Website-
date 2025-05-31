using E_Commerce.Application.Dtos.OrderItemDtos;
using E_Commerce.Application.Interfaces;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Usecases.OrderItemServices
{
    public class OrderItemServices : IOrderItemServices
    {
        private readonly IRepository<OrderItem> _repository;
        public OrderItemServices(IRepository<OrderItem> repository)
        {
            _repository = repository;
        }

        public async Task CreateOrderItemAsync(CreateOrderItemDto createOrderItemDto)
        {
            await _repository.CreateAsync(new OrderItem
            {
                //OrderID = createOrderItemDto.OrderID,
                ProductID = createOrderItemDto.ProductID,
                Quantity = createOrderItemDto.Quantity,
                TotalPrice = createOrderItemDto.TotalPrice,

            });
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var deleteOrderItem = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(deleteOrderItem);
        }

        public async Task<List<ResultOrderItemDto>> GetAllOrderItemAsync()
        {
           var values = await _repository.GetAllAsync();
            return values.Select(x => new ResultOrderItemDto
            {
                OrderItemID = x.OrderItemID,
                OrderID = x.OrderID,
                ProductID = x.ProductID,
                Quantity = x.Quantity,
                TotalPrice = x.TotalPrice,
            }).ToList();
        }

        public async Task<GetByIdOrderItemDto> GetByIdAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            var result = new GetByIdOrderItemDto
            {
                OrderItemID = values.OrderItemID,
                OrderID = values.OrderID,
                ProductID = values.ProductID,
                Quantity = values.Quantity,
                TotalPrice = values.TotalPrice,
            };
            return result;
        }

        public async Task UpdateOrderItemAsync(UpdateOrderItemDto model)
        {
            var values = await _repository.GetByIdAsync(model.OrderItemID);
            values.OrderItemID = model.OrderItemID;
            //values.OrderID = model.OrderID;
            values.ProductID = model.ProductID;
            values.Quantity = model.Quantity;
            values.TotalPrice = model.TotalPrice;
        }
    }
}
