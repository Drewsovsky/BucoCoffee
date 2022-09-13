using BucoCoffee.Models;
using BucoCoffee.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucoCoffee.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ObservableCollection<ProductItem> ProductItemsList { get; set; }

        public ICommand GotoNewItemPageCommand => new Command(GotoNewItemPage);
        public ICommand GotoSettingsPageCommand => new Command(GotoSettingsPage);

        public MainPageViewModel(INavigation navigation) 
        {
            Navigation = navigation;

            Init();
        }

        public async void Init()
        {

            ProductItemsList = new ObservableCollection<ProductItem>(await _firebaseHelper.GetAllProductItems());

            OnPropertyChanged(nameof(ProductItemsList));
        }

        async private void GotoNewItemPage()
        {
            await Navigation.PushAsync(new NewItemPage());
        }

        async private void GotoSettingsPage()
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}
