using ShopOnline.Models.Dtos;

namespace BlazorApp1.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<CartItemDto>> getItems(int userId);
        Task<CartItemDto> GetItem(int id);
        Task<CartItemDto> DeleteItem(int id);
        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItemDto> UpdateQuantity(CartItemQtyUpdateDto updateDto);

        event Action<int> OnShoppingCartChanged;
        void RaiseEventOnShoppingCartChanged(int totalQty);
    }
}
