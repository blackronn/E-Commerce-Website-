    using E_Commerce.Application.Dtos.CartDtos;
using E_Commerce.Application.Dtos.CartItemDtos;
using E_Commerce.Application.Interfaces;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace E_Commerce.Application.Usecases.CartServices
{
    public class CartServices : ICartServices
    {
        private readonly IRepository<Cart> _repository;
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Product> _productRepository;

        public CartServices(IRepository<CartItem> cartItemRepository,IRepository<Cart> repository,IRepository<Customer> customerRepository,IRepository<Product> productRepository)
        {
            _cartItemRepository = cartItemRepository;
            _repository = repository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task CreateCartAsync(CreateCartDto createCartDto)
        {
            var cart = new Cart
            {
                //TotalAmount = createCartDto.TotalAmount,
                CreatedDate = DateTime.Now,
                CustomerID = createCartDto.CustomerID,
            };
            await _repository.CreateAsync(cart);
            decimal sum = 0;
            foreach (var item in createCartDto.CartItems)
            {
                var cartItem = new CartItem
                {
                    CartID = cart.CartID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                };
                sum = sum + (item.Quantity * item.TotalPrice);
                await _cartItemRepository.CreateAsync(cartItem);
            }
            cart.TotalAmount = sum;
            await _repository.UpdateAsync(cart);
        }

        public async Task DeleteCartAsync(int id)
        {
            var cart = await _repository.GetByIdAsync(id);
            var cartItems = await _cartItemRepository.GetAllAsync();
            foreach (var item in cartItems.Where(x => x.CartID == id))
            {
                await _cartItemRepository.DeleteAsync(item);
            }
            await _repository.DeleteAsync(cart);
        }

        public async Task<List<ResultCartDto>> GetAllCartAsync()
        {
            var carts = await _repository.GetAllAsync();
            var cartItems = await _cartItemRepository.GetAllAsync();
            var product = await _productRepository.GetAllAsync();
            var result = new List<ResultCartDto>();
            foreach (var item in carts)
            {
                var customerdto = await _customerRepository.GetByFilterAsync(cus => cus.CustomerID == item.CustomerID);
                var cartDto = new ResultCartDto
                {
                    CartID = item.CartID,
                    TotalAmount = item.TotalAmount,
                    CreatedDate = item.CreatedDate,
                    Customer = customerdto,
                    CustomerID = item.CustomerID,
                    CartItems = new List<ResultCartItemDto>()
                };
                foreach (var item1 in item.CartItems)
                {
                    var prodcutdto = await _productRepository.GetByFilterAsync(prd => prd.ProductID == item1.ProductID);
                    var cartItemddto = new ResultCartItemDto
                    {
                        CartID = item1.CartID,
                        CartItemID = item1.CartItemID,
                        ProductID = item1.ProductID,
                        Product = prodcutdto,
                        Quantity = item1.Quantity,
                        TotalPrice = item1.TotalPrice,
                    };
                    cartDto.CartItems.Add(cartItemddto);
                    

                }
                result.Add(cartDto);
                
            }
            return result;
        }

        public async Task<GetByIdCartDto> GetCartByIdAsync(int id)
        {
            var cart = await _repository.GetByIdAsync(id);
            var customer = await _customerRepository.GetByIdAsync(id);
            var cartItem = await _cartItemRepository.GetAllAsync();
            var customerdto = await _customerRepository.GetByFilterAsync(cus => cus.CustomerID == cart.CustomerID);
            var result = new GetByIdCartDto
            {
                CartID = cart.CartID,
                TotalAmount = cart.TotalAmount,
                CreatedDate = cart.CreatedDate,
                CartItems = new List<ResultCartItemDto>(),
                CustomerID = cart.CustomerID,
                Customer = customerdto,

            };
            foreach (var item1 in cart.CartItems)
            {
                var product = await _productRepository.GetByIdAsync(item1.ProductID);
                var cartItemDto = new ResultCartItemDto
                {
                    CartItemID = item1.CartItemID,
                    CartID = item1.CartID,
                    ProductID = item1.ProductID,
                    Product = product,
                    Quantity = item1.Quantity,
                    TotalPrice = item1.TotalPrice
                };
                result.CartItems.Add(cartItemDto);
            }

            return result;
        }

        public async Task UpdateCartAsync(UpdateCartDto updateCartDto)
        {
            var cart = await _repository.GetByIdAsync(updateCartDto.CartID);
            var cartitem = await _cartItemRepository.GetAllAsync();
            //cart.CreatedDate = updateCartDto.CreatedDate;
            //cart.TotalAmount = updateCartDto.TotalAmount;
            //cart.CustomerID = updateCartDto.CustomerID;
            var sum = 0m;
            foreach (var item1 in updateCartDto.CartItems)
            { 
                foreach (var item in cart.CartItems)
                {
                var cartItem = await _cartItemRepository.GetByIdAsync(item.CartItemID);
               
                
                    if (cartItem.CartItemID == item1.CartItemID)
                    {
                        cartItem.Quantity = item1.Quantity;
                        cartItem.TotalPrice = item1.TotalPrice;
                    }
                    sum = sum + item.TotalPrice;


                        
                }
               


            }
            cart.TotalAmount = sum;
            await _repository.UpdateAsync(cart);
        }

    }
}
