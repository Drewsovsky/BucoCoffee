using BucoCoffee.Models;
using BucoCoffee.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BucoCoffee.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public NewItemPage(ProductItem productItem = null)
        {
            InitializeComponent();

            BindingContext = new NewItemPageViewModel(Navigation, productItem);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as NewItemPageViewModel).OnAppearing();
        }
    }
}