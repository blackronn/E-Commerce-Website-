using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Dtos.CartItemDtos
{
    public class ResultCartItemDto
    {
        public int CartItemID { get; set; }
 
        public int ProductID { get; set; }
        public int CartID { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
