using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Dtos.CartItemDtos
{
    public class CreateCartItemDto
    {
        public int Quantity { get; set; }
        public int ProductID { get; set; }
        public int CartID { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
