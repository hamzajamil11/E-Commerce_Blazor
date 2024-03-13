using BlazorApp1.Services;
using BlazorApp1.Services.Contracts;
using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;

namespace BlazorApp1.Pages
{
    public class ProductBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public IEnumerable<ProductDto> Products { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public List<CartItemDto> Items { get; set; }
        public string ErrorMessage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Products = await ProductService.GetItems();
                var Item = await ShoppingCartService.getItems(1);
                Items = Item.ToList();
                var totalQty = Items.Sum(i => i.Qty);
                ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);
                //Console.WriteLine(Products);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;

            }

        }

        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory()
        {
            return from product in Products
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }
        protected string GetCategoryName(IGrouping<int, ProductDto> groupedProductDtos)
        {
            return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key).CategoryName;
        }
    }

}
