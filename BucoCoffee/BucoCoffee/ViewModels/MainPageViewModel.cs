using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucoCoffee.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel() 
        {
        }

        public ICommand GotoNewItemPageCommand => new Command(GotoNewItemPage);

        private void GotoNewItemPage()
        {
            Console.WriteLine("+++");
        }
    }
}
