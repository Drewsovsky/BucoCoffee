using BucoCoffee.Models;
using BucoCoffee.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucoCoffee.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ObservableCollection<ProductItem> ProductItemsList { get; set; }
        public ObservableCollection<ProductItem> Products { get; set; }

        private string _selectedDate;
        public string SelectedDate
        {
            get { return _selectedDate; }
            set { 
                _selectedDate = DateTime.Parse(value).ToShortDateString();

                if (Products != null) { Products.Clear(); }

                Products = new ObservableCollection<ProductItem>(ProductItemsList.Where(product => product.PackingDate.Equals(SelectedDate)));

                OnPropertyChanged(nameof(Products));
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        public ICommand GotoNewItemPageCommand => new Command(GotoNewItemPage);
        public ICommand GotoSettingsPageCommand => new Command(GotoSettingsPage);
        public ICommand PastDateCommand => new Command(PastDate);
        public ICommand FutureDateCommand => new Command(FutureDate);

        public MainPageViewModel(INavigation navigation) 
        {
            Navigation = navigation;

            Init();
        }

        public async void Init()
        {
            ProductItemsList = new ObservableCollection<ProductItem>(await _firebaseHelper.GetAllProductItems());
            SelectedDate = DateTime.Now.ToString();

            // OnPropertyChanged(nameof(ProductItemsList));
        }

        async private void GotoNewItemPage()
        {
            await Navigation.PushAsync(new NewItemPage());
        }

        async private void GotoSettingsPage()
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private void PastDate()
        {
            SelectedDate = DateTime.Parse(SelectedDate).AddDays(-1).ToShortDateString();
        }

        private void FutureDate()
        {
            SelectedDate = DateTime.Parse(SelectedDate).AddDays(1).ToShortDateString();
        }
    }
}
