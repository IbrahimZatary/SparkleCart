using SparkeApp.DTOs.Cart;

namespace SparkeApp.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartResponseDto> AddToCartAsync(int id, AddToCartRequestDto requestDto);
        Task<CartResponseForUserDto> GetCartByUser(int id);
        Task<UpdateQResponseDto> UpdateQuantityAsync(UpdateQDto updateQDto);
        Task <OrderResponseDto> CheckoutAsync(CheckoutRequestDto request); 
    }
}
