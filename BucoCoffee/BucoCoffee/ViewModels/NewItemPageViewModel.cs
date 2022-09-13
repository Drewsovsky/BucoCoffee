using BucoCoffee.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucoCoffee.ViewModels
{
    public class NewItemPageViewModel : BaseViewModel
    {
        public ObservableCollection<ProductType> ProductTypesList { get; set; }
        public string Packer { get; set; }
        public string PackingDate { get; set; }
        public string PackageDate { get; set; }
        public int PackageAmount { get; set; }
        public double PackageWeight { get; set; }
        public string Comment { get; set; }
        private ProductType _selectedProductType;

        public ProductType SelectedProductType
        {
            get { return _selectedProductType; }
            set { _selectedProductType = value; }
        }

        public ICommand AddProductItemCommand => new Command(AddProductItem);

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

        public async void AddProductItem()
        {
            if (SelectedProductType != null &&
                !String.IsNullOrWhiteSpace(Packer) &&
                !String.IsNullOrWhiteSpace(PackageDate) &&
                !String.IsNullOrWhiteSpace(PackingDate) &&
                PackageAmount > 0 &&
                PackageWeight > 0)
            {
                Guid productTypeId = SelectedProductType.Id;

                await _firebaseHelper.AddProductItem(productTypeId, Comment, PackageDate, PackingDate, PackageAmount, Packer, PackageWeight);
            }
            else
            {
                var a = 1;
            }
        }
    }
}
