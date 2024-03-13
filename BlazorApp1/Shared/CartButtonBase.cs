using BlazorApp1.Services;
using BlazorApp1.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Shared
{
    public class CartButtonBase: ComponentBase, IDisposable
    {
        [Inject]
        public IShoppingCartService ShoppingCartService {get; set;}
        public int shoppingCartItemCount { get; set; }
        protected override void OnInitialized()
        {
            if (ShoppingCartService != null)
            {
                ShoppingCartService.OnShoppingCartChanged += shoppingCartChanged;
            }
        }
        public void shoppingCartChanged(int qty)
        {
            shoppingCartItemCount = qty;
            StateHasChanged();
        }
        void IDisposable.Dispose()
        {
            if (ShoppingCartService != null)
            {
                ShoppingCartService.OnShoppingCartChanged -= shoppingCartChanged;
            }
        }
    }
}
