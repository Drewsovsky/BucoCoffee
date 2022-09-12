using BucoCoffee.Views;
using System;
using System.Collections.Generic;
using System.Text;
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

        async private void GotoNewItemPage()
        {
            await Navigation.PushAsync(new NewItemPage());
        }
    }
}
