using E_Commerce.Application.Dtos.OrderDtos;
using E_Commerce.Application.Dtos.OrderItemDtos;
using E_Commerce.Application.Interfaces;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Usecases.OrderServices
{
    public class OrderServices: IOrderServices
    {
        private readonly IRepository<Order> _repository;
        private readonly IRepository<OrderItem> _repositoryOrderItem;

        public OrderServices(IRepository<OrderItem> repositoryOrderItem, IRepository<Order> repository)
        {
            _repositoryOrderItem = repositoryOrderItem;
            _repository = repository;
        }

        public async Task CreateOrderAsync(CreateOrderDto model)
        {
            var order = new Order
            {
                OrderDate = model.OrderDate,
                TotalAmount = model.TotalAmount,
                OrderStatus = model.OrderStatus,
                //BillingAdress = model.BillingAdress,
                ShippingAdress = model.ShippingAdress,
                PaymentMethod = model.PaymentMethod,
                CustomerID = model.CustomerID,
            };
            await _repository.CreateAsync(order);
            foreach (var item in model.OrderItems)
            {
                await _repositoryOrderItem.CreateAsync( new OrderItem
                {
                    OrderID = order.OrderID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                });

            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(customer);
        }

        public async Task<List<ResultOrderDto>> GetAllOrderAsync()
        {
            var values = await _repository.GetAllAsync();
            var orderitem = await _repositoryOrderItem.GetAllAsync();
            return values.Select(x => new ResultOrderDto
            {
                OrderID = x.OrderID,
                OrderDate = x.OrderDate,
                TotalAmount = x.TotalAmount,
                OrderStatus = x.OrderStatus,
                //BillingAdress = x.BillingAdress,
                ShippingAdress = x.ShippingAdress,
                PaymentMethod = x.PaymentMethod,
                CustomerID = x.CustomerID,
                OrderItems = x.OrderItems.Select(z => new ResultOrderItemDto
                {
                    OrderID = z.OrderID,
                    ProductID = z.ProductID,
                    Quantity = z.Quantity,
                    TotalPrice = z.TotalPrice,
                    OrderItemID = z.OrderItemID,
                }).ToList(),
            }).ToList();
        }

        public async Task<GetByIdOrderDto> GetByIdAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            return new GetByIdOrderDto
            {
                OrderID = values.OrderID,
                OrderDate = values.OrderDate,
                TotalAmount = values.TotalAmount,
                OrderStatus = values.OrderStatus,
                //BillingAdress = values.BillingAdress,
                ShippingAdress = values.ShippingAdress,
                PaymentMethod = values.PaymentMethod,
                CustomerID = values.CustomerID,
            };
        }

        public async Task UpdateOrderAsync(UpdateOrderDto model)
        {
            var values = await _repository.GetByIdAsync(model.OrderID);
            values.OrderID = model.OrderID;
            values.OrderDate = model.OrderDate;
            values.TotalAmount = model.TotalAmount;
            values.OrderStatus = model.OrderStatus;
            //values.BillingAdress = model.BillingAdress;
            values.ShippingAdress = model.ShippingAdress;
            values.PaymentMethod = model.PaymentMethod;
            values.CustomerID = model.CustomerID;
            await _repository.UpdateAsync(values);
        }
    }
}
