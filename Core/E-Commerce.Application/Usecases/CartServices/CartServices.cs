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

namespace E_Commerce.Application.Usecases.CartServices
{
    public class CartServices : ICartServices
    {
        private readonly IRepository<Cart> _repository;
        private readonly IRepository<CartItem> _cartItemRepository;

        public CartServices(IRepository<CartItem> cartItemRepository,IRepository<Cart> repository)
        {
            _cartItemRepository = cartItemRepository;
            _repository = repository;
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
            await _repository.DeleteAsync(cart);
        }

        public async Task<List<ResultCartDto>> GetAllCartAsync()
        {
            var values = await _repository.GetAllAsync();
            var cartitems = await _cartItemRepository.GetAllAsync();
            return values.Select(x => new ResultCartDto
            {
                CartID = x.CartID,
                TotalAmount = x.TotalAmount,
                CreatedDate = x.CreatedDate,
                CustomerID = x.CustomerID,
                CartItems = x.CartItems.Select(y => new ResultCartItemDto
                {
                    CartItemID = y.CartItemID,
                    CartID = y.CartID,
                    ProductID = y.ProductID,
                    Quantity = y.Quantity,
                    TotalPrice = y.TotalPrice
                }).Where(y => y.CartID == x.CartID).ToList()
            }).ToList();
        }

        public async Task<GetByIdCartDto> GetCartByIdAsync(int id)
        {
            var cart = await _repository.GetByIdAsync(id);
            
            return new GetByIdCartDto
            {
                CartID = cart.CartID,
                TotalAmount = cart.TotalAmount,
                CreatedDate = cart.CreatedDate,
                CustomerID = cart.CustomerID,
                
            };
        }

        public async Task UpdateCartAsync(UpdateCartDto updateCartDto)
        {
            var cart = await _repository.GetByIdAsync(updateCartDto.CartID);
            cart.CreatedDate = updateCartDto.CreatedDate;
            cart.TotalAmount = updateCartDto.TotalAmount;
            cart.CustomerID = updateCartDto.CustomerID;
            await _repository.UpdateAsync(cart);

        }

    }
}
