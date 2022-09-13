using BucoCoffee.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucoCoffee.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public ObservableCollection<ProductType> ProductTypesList { get; set; }

        public ICommand AddProductTypeCommand => new Command(AddProductType);

        public SettingsPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();

            ProductTypesList = new ObservableCollection<ProductType>(await _firebaseHelper.GetAllProductTypes());
            OnPropertyChanged(nameof(ProductTypesList));
        }

        private async void AddProductType()
        {
            await _firebaseHelper.AddProductType("test","#ff0000");
        }
    }
}
