using E_Commerce.Application.Dtos.OrderDtos;
using E_Commerce.Application.Dtos.OrderItemDtos;
using E_Commerce.Application.Interfaces;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace E_Commerce.Application.Usecases.OrderServices
{
    public class OrderServices: IOrderServices
    {
        private readonly IRepository<Order> _repository;
        private readonly IRepository<OrderItem> _repositoryOrderItem;
        private readonly IRepository<Customer> _repositoryCustomer;
        private readonly IRepository<Product> _repositoryProduct;

        public OrderServices(IRepository<OrderItem> repositoryOrderItem, IRepository<Order> repository, IRepository<Customer> repositoryCustomer, IRepository<Product> repositoryProduct)
        {
            _repositoryOrderItem = repositoryOrderItem;
            _repository = repository;
            _repositoryCustomer = repositoryCustomer;
            _repositoryProduct = repositoryProduct;
        }

        public async Task CreateOrderAsync(CreateOrderDto model)
        {
            decimal sum = 0;
            var order = new Order
            {
                OrderDate = model.OrderDate,
                TotalAmount = sum,
                OrderStatus = model.OrderStatus,
                //BillingAdress = model.BillingAdress,
                ShippingAdress = model.ShippingAdress,
                //PaymentMethod = model.PaymentMethod,
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
                sum = sum + item.TotalPrice;
            }
            order.TotalAmount = sum;
            await _repository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            foreach(var item in values.OrderItems)
            {
                var orderItem = await _repositoryOrderItem.GetByIdAsync(item.OrderItemID);
                await _repositoryOrderItem.DeleteAsync(orderItem);


            }
            await _repository.DeleteAsync(values);
        }

        public async Task<List<ResultOrderDto>> GetAllOrderAsync()
        {
            var values = await _repository.GetAllAsync();
            var orderitem = await _repositoryOrderItem.GetAllAsync();
            var result = new List<ResultOrderDto>();
            foreach (var item in values)
            {
                var customer = await _repositoryCustomer.GetByIdAsync(item.CustomerID);
                var orderdto = new ResultOrderDto
                {
                    OrderID = item.OrderID,
                    OrderDate = item.OrderDate,
                    TotalAmount = item.TotalAmount,
                    OrderStatus = item.OrderStatus,
                    //BillingAdress = item.BillingAdress,
                    ShippingAdress = item.ShippingAdress,
                    //PaymentMethod = item.PaymentMethod,
                    CustomerID = item.CustomerID,
                    Customer = customer,
                    OrderItems = new List<ResultOrderItemDto>()

                };
                foreach(var item1 in item.OrderItems)
                {
                    var orderItemProduct = await _repositoryProduct.GetByIdAsync(item1.ProductID);
                    var orderItemDto = new ResultOrderItemDto
                    {
                        OrderID = item1.OrderID,
                        ProductID = item1.ProductID,
                        Quantity = item1.Quantity,
                        TotalPrice = item1.TotalPrice,
                        OrderItemID = item1.OrderItemID,
                        Product = orderItemProduct
                    };
                    orderdto.OrderItems.Add(orderItemDto);

                }
                result.Add(orderdto);
                return result;
            }
            return values.Select(x => new ResultOrderDto
            {
                OrderID = x.OrderID,
                OrderDate = x.OrderDate,
                TotalAmount = x.TotalAmount,
                OrderStatus = x.OrderStatus,
                //BillingAdress = x.BillingAdress,
                ShippingAdress = x.ShippingAdress,
                //PaymentMethod = x.PaymentMethod,
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
            var customer = await _repositoryCustomer.GetByIdAsync(values.CustomerID);
            var result= new GetByIdOrderDto
            {
                OrderID = values.OrderID,
                OrderDate = values.OrderDate,
                TotalAmount = values.TotalAmount,
                OrderStatus = values.OrderStatus,
                //BillingAdress = values.BillingAdress,
                ShippingAdress = values.ShippingAdress,
                //PaymentMethod = values.PaymentMethod,
                CustomerID = values.CustomerID,
                Customer = customer,
                OrderItems = new List<ResultOrderItemDto>()
            };
            foreach (var item in result.OrderItems)
            { 
            var orderitemproduct = await _repositoryProduct.GetByIdAsync(item.ProductID);
                var orderItemDto = new ResultOrderItemDto
                {
                    OrderID = item.OrderID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                    OrderItemID = item.OrderItemID,
                    Product = orderitemproduct
                };
                result.OrderItems.Add(orderItemDto);


            }
            return result;
        }

        public async Task UpdateOrderAsync(UpdateOrderDto model)
        {
            var values = await _repository.GetByIdAsync(model.OrderID);
            var orderitems = await _repositoryOrderItem.GetAllAsync();
            values.OrderStatus = model.OrderStatus;
            decimal sum = 0;
            foreach (var item in model.OrderItems)
            {

                foreach (var item1 in values.OrderItems)
                {
                    var orderItemdto = await _repositoryOrderItem.GetByIdAsync(item1.OrderItemID);
                    if (item1.OrderItemID == item.OrderItemID)
                    {
                        orderItemdto.Quantity = item.Quantity;
                        orderItemdto.TotalPrice = item.TotalPrice;
                       
                    } 
                    sum = sum + item1.TotalPrice;

                }
            
            
            }
            values.TotalAmount = sum;
      
            await _repository.UpdateAsync(values);
        }
    }
}
