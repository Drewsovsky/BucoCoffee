using BucoCoffee.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BucoCoffee.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public NewItemPage()
        {
            InitializeComponent();

            BindingContext = new NewItemPageViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as NewItemPageViewModel).OnAppearing();
        }
    }
}