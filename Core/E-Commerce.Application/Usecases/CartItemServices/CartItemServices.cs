using E_Commerce.Application.Dtos.CartItemDtos;
using E_Commerce.Application.Interfaces;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace E_Commerce.Application.Usecases.CartItemServices
{
    public class CartItemServices : ICartItemServices
    {
        private readonly IRepository<CartItem> _repository;
        public CartItemServices(IRepository<CartItem> repository)
        {
            _repository = repository;
        }

        public Task CreateCartItemAsync(CreateCartItemDto createCartItemDto)
        {
            var cartitem = new CartItem
            {
                Quantity = createCartItemDto.Quantity,
                ProductID = createCartItemDto.ProductID,
                CartID = createCartItemDto.CartID,
                TotalPrice = createCartItemDto.TotalPrice
            };
            return _repository.CreateAsync(cartitem);
        }

        public async Task DeleteCartItemAsync(int id)
        {
            var cartItem = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(cartItem);
        }

        public async Task<List<ResultCartItemDto>> GetAllCartItemAsync()
        {
            var cartItems = await _repository.GetAllAsync();
            return cartItems.Select(x => new ResultCartItemDto
            {
                CartItemID = x.CartItemID,
                Quantity = x.Quantity,
                ProductID = x.ProductID,
                CartID = x.CartID,
                TotalPrice = x.TotalPrice
            }).ToList();


        }

        public Task<List<ResultCartItemDto>> GetByCartIdCartItemsAsync(int cartId)
        {
            throw new NotImplementedException();
        }

        public async Task<GetByIdCartItemDto> GetCartItemByIdAsync(int id)
        {
            var cartItem = await _repository.GetByIdAsync(id);
            return new GetByIdCartItemDto
            {
                CartItemID = cartItem.CartItemID,
                Quantity = cartItem.Quantity,
                ProductID = cartItem.ProductID,
                CartID = cartItem.CartID,
                TotalPrice = cartItem.TotalPrice
            };
        }

        public async Task UpdateCartItemAsync(UpdateCartItemDto updateCartItemDto)
        {
           var cartItem = await _repository.GetByIdAsync(updateCartItemDto.CartItemID);
           
                cartItem.Quantity = updateCartItemDto.Quantity;
                cartItem.ProductID = updateCartItemDto.ProductID;
                //cartItem.CartID = updateCartItemDto.CartID;
                cartItem.TotalPrice = updateCartItemDto.TotalPrice;
                await _repository.UpdateAsync(cartItem);
        }

    }
}
