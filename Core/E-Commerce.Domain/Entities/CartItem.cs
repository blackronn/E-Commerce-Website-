using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities
{
    public class CartItem
    {
        public int CartItemID { get; set; }
        public int Quantity { get; set; } 
        public int ProductID { get; set; }
        public int CartID { get; set; }

        public decimal TotalPrice { get; set; }
        // Navigation properties
        //public Product Product { get; set; }
        //public Cart Cart { get; set; }
    }
}
