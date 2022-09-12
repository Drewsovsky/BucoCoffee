using BucoCoffee.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucoCoffee.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigation navigation) 
        {
            Navigation = navigation;
        }

        public ICommand GotoNewItemPageCommand => new Command(GotoNewItemPage);
        public ICommand GotoSettingsPageCommand => new Command(GotoSettingsPage);

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
