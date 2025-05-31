using E_Commerce.Application.Dtos.CartItemDtos;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Dtos.CartDtos
{
    public class GetByIdCartDto
    {
        public int CartID { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public ICollection<ResultCartItemDto> CartItems { get; set; }
    }
}
