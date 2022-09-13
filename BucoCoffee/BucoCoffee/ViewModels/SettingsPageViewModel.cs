using System.Windows.Input;
using Xamarin.Forms;

namespace BucoCoffee.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public SettingsPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        public ICommand AddProductTypeCommand => new Command(AddProductType);

        private async void AddProductType()
        {
            await _firebaseHelper.AddProductType("test","ff0000");
        }
    }
}
