using E_Commerce.Application.Dtos.OrderItemDtos;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Dtos.OrderDtos
{
    public class ResultOrderDto
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        //public string BillingAdress { get; set; }
        public string ShippingAdress { get; set; }
        public string PaymentMethod { get; set; }
        public int CustomerID { get; set; }
        //public Customer Customer { get; set; }
        public ICollection<ResultOrderItemDto> OrderItems { get; set; }
    }
}
