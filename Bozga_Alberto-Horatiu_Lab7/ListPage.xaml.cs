using Bozga_Alberto_Horatiu_Lab7.Models;

namespace Bozga_Alberto_Horatiu_Lab7;

public partial class ListPage : ContentPage
{
    public ListPage()
    {
        InitializeComponent();
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            await App.Database.DeleteShopListAsync(slist);
            await Navigation.PopAsync();
        }
    }
        protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
        async void OnDeleteItemButtonClicked(object sender, EventArgs e)
        {
            var selectedProduct = listView.SelectedItem as Product;

            if (selectedProduct != null)
            { 
                var currentShoppingList = (ShopList)BindingContext;

                await App.Database.DeleteListProductAsync(currentShoppingList.ID, selectedProduct.ID);

                listView.ItemsSource = await App.Database.GetListProductsAsync(currentShoppingList.ID);
            }
        }

    }

}