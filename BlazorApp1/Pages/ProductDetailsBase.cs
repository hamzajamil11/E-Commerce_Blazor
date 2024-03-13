using BlazorApp1.Services.Contracts;
using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;

namespace BlazorApp1.Pages
{
    public class ProductDetailsBase:ComponentBase
    {
        [Parameter]
        public int id { get; set; }
        public ProductDto Product { get; set; }
        public string ErrorMessage { get; set; }
        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Product = await ProductService.GetItem(id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;

            }

        }
        public async Task AddToCart(CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var cartItemDto = await ShoppingCartService.AddItem(cartItemToAddDto);
                NavigationManager.NavigateTo("/ShoppingCart");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
