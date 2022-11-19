using BucoCoffee.Global;
using BucoCoffee.Models;
using BucoCoffee.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
            get => _selectedDate;
            set { 
                _selectedDate = DateTime.Parse(value).ToShortDateString();

                if (Products != null) { Products.Clear(); }

                Products = new ObservableCollection<ProductItem>(ProductItemsList.Where(product => product.PackingDate.Equals(SelectedDate)));

                OnPropertyChanged(nameof(Products));
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private bool _isRefresging;
        public bool IsRefreshing
        {
            get => _isRefresging;
            set
            {
                _isRefresging = value;

                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        private ProductItem _selectedProduct;
        public ProductItem SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                

            }
        }

        public ICommand GotoNewItemPageCommand => new Command(GotoNewItemPage);
        public ICommand GotoSettingsPageCommand => new Command(GotoSettingsPage);
        public ICommand PastDateCommand => new Command(PastDate);
        public ICommand FutureDateCommand => new Command(FutureDate);
        public ICommand RefreshProductsCommand => new Command(RefreshProducts);

        public MainPageViewModel(INavigation navigation) 
        {
            Navigation = navigation;

            Subscribe();

            Init();
        }

        ~MainPageViewModel() {
            MessagingCenter.Unsubscribe<MainPageViewModel, string>(this, Constants.MessagingCenter.AddedProduct);
        }

        private void Subscribe()
        {
            MessagingCenter.Subscribe<Application>(this, Constants.MessagingCenter.AddedProduct, (sender) =>
            {
                RefreshProducts();
            });
        }

        private async void RefreshProducts()
        {
            IsRefreshing = true;

            ProductItemsList.Clear();
            Products.Clear();

            ProductItemsList = new ObservableCollection<ProductItem>(await _firebaseHelper.GetAllProductItems());
            SortProductsList();

            IsRefreshing = false;
        }

        public async void Init()
        {
            ProductItemsList = new ObservableCollection<ProductItem>(await _firebaseHelper.GetAllProductItems());

            SortProductsList();
        }

        private void SortProductsList()
        {
            SelectedDate = DateTime.Now.ToString();
        }

        async private void GotoNewItemPage()
        {
            await Navigation.PushAsync(new NewItemPage());
        }

        async private void GotoEditItemPage() // Same as new item page style
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
