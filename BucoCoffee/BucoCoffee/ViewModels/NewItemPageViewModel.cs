using BucoCoffee.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace BucoCoffee.ViewModels
{
    public class NewItemPageViewModel : BaseViewModel
    {
        public ObservableCollection<ProductType> ProductTypesList { get; set; }

        public NewItemPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        public async override void OnAppearing()
        {
            base.OnAppearing();

            ProductTypesList = new ObservableCollection<ProductType>(await _firebaseHelper.GetAllProductTypes());

            OnPropertyChanged(nameof(ProductTypesList));
        }
    }
}
