using BlazorApp1.Services.Contracts;
using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;

namespace BlazorApp1.Pages
{
    public class ShoppingCartBase:ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        public IEnumerable<CartItemDto> Item { get; set; }
        public List<CartItemDto> Items { get; set; }
        public string ErrorMessage { get; set; }
        public string TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Item = await ShoppingCartService.getItems(1);
                Items = Item.ToList();
                TotalPrice = SetTotalPrice();
                TotalQuantity = SetTotalQuantity();
                ShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }
        public async Task DeleteItem(int id)
        {
            var cartItemDto = await ShoppingCartService.DeleteItem(id);
            var itemToDelete = Items.FirstOrDefault(item => item.Id == id);
            if (itemToDelete != null)
            {
                Items.Remove(itemToDelete);
            }
            TotalPrice = SetTotalPrice();
            TotalQuantity = SetTotalQuantity();
            ShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
        }
        public string SetTotalPrice()
        {
            
            TotalPrice = Items.Sum(p => p.TotalPrice).ToString("C");
            return TotalPrice;

        }
        public void CalculateTotalPrice(CartItemDto cartItemDto)
        {
            cartItemDto.TotalPrice = cartItemDto.Price * cartItemDto.Qty;
        }
        private int SetTotalQuantity()
        {
            return TotalQuantity = Items.Sum(p => p.Qty);
        }
        public async Task UpdateQty(int Id, int qty)
        {
            var updatedItem = new CartItemQtyUpdateDto
            {
                CartItemId = Id,
                Qty = qty
            };

            var returnedUpdateItemDto = await ShoppingCartService.UpdateQuantity(updatedItem);
            foreach (var item in Items)
            {
                if (item.Id == returnedUpdateItemDto.Id)
                {
                    item.Qty = returnedUpdateItemDto.Qty;
                    CalculateTotalPrice(item);
                    break;
                }
            }
            TotalPrice = SetTotalPrice();
            TotalQuantity = SetTotalQuantity();
            ShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
        }
    }
    
}
