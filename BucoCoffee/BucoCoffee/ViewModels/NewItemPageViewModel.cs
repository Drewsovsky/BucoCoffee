using BucoCoffee.Global;
using BucoCoffee.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucoCoffee.ViewModels
{
    public class NewItemPageViewModel : BaseViewModel
    {
        public const int MAX_WEIGHT = 1_000_000;
        public const double MAX_AMOUNT = 1_000_000.0f;

        public ObservableCollection<ProductType> ProductTypesList { get; set; }
        public ObservableCollection<PackingType> PackingTypesList { get; set; }
        public string Packer { get; set; }

        private string _packingDate;
        public string PackingDate
        {
            get { return _packingDate; }
            set { _packingDate = DateTime.Parse(value).ToShortDateString(); }
        }

        private string _packageDate;
        public string PackageDate
        {
            get { return _packageDate; }
            set { _packageDate = DateTime.Parse(value).ToShortDateString(); }
        }
        public int PackageAmount { get; set; }
        public double PackageWeight { get; set; }
        public string Comment { get; set; }

        public ProductType SelectedProductType { get; set; }
        public PackingType SelectedPackingType { get; set; }
        public ProductItem DTOSelectedProduct { get; set; }
        public bool IsEditMode { get; set; } = false;

        public ICommand AddProductItemCommand => new Command(AddProductItem);
        public ICommand EditProductItemCommand => new Command(EditProductItem);
        public ICommand TakePhotoCommand => new Command(TakePhoto);
        // TODO:
        private void TakePhoto()
        {

        }

        public ICommand SelectFromGalleryCommand => new Command(SelectFromGallery);
        // TODO:
        private void SelectFromGallery()
        {

        }

        public NewItemPageViewModel(INavigation navigation, ProductItem productItem)
        {
            Navigation = navigation;

            PackingDate = DateTime.Now.ToString();
            PackageDate = DateTime.Now.ToString();

            if (productItem != null)
            {
                DTOSelectedProduct = productItem;
                SetProductItemFields(DTOSelectedProduct);
            }
        }

        public async override void OnAppearing()
        {
            base.OnAppearing();

            ProductTypesList = new ObservableCollection<ProductType>(await _firebaseHelper.GetAllProductTypes());
            PackingTypesList = new ObservableCollection<PackingType>(await _firebaseHelper.GetAllPackingTypes());

            OnPropertyChanged(nameof(ProductTypesList));
            OnPropertyChanged(nameof(PackingTypesList));

            if (DTOSelectedProduct != null)
            {
                SelectedProductType = DTOSelectedProduct.ProductKeyType;
                SelectedPackingType = DTOSelectedProduct.PackingKeyType;

                OnPropertyChanged(nameof(SelectedProductType));
                OnPropertyChanged(nameof(SelectedPackingType));
            }
        }

        private void SetProductItemFields(ProductItem dto)
        {
            Packer = dto.PackerName;
            PackageAmount = dto.PackageAmount;
            PackageWeight = dto.Weight;
            Comment = dto.Comment;
            PackingDate = dto.PackingDate;
            PackageDate = dto.PackageDate;
        }

        public async void AddProductItem()
        {
            try
            {
                if (SelectedProductType != null &&
                SelectedPackingType != null &&
                !String.IsNullOrWhiteSpace(Packer) &&
                !String.IsNullOrWhiteSpace(PackageDate) &&
                !String.IsNullOrWhiteSpace(PackingDate) &&
                (PackageAmount > 0 || PackageWeight > 0))
                {
                    Guid productTypeId = SelectedProductType.Id;
                    Guid packingTypeId = SelectedPackingType.Id;

                    await _firebaseHelper.AddProductItem(
                        productTypeId,
                        packingTypeId,
                        Comment,
                        PackageDate,
                        PackingDate,
                        PackageAmount,
                        Packer,
                        PackageWeight);

                    MessagingCenter.Send(App.Current, Constants.MessagingCenter.AddedProduct);

                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    // TODO: Errors
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error found: " + ex);
            }
        }

        public async void EditProductItem()
        {
            try
            {
                if (SelectedProductType != null &&
                    SelectedPackingType != null &&
                    !String.IsNullOrWhiteSpace(Packer) &&
                    !String.IsNullOrWhiteSpace(PackageDate) &&
                    !String.IsNullOrWhiteSpace(PackingDate) &&
                    (PackageAmount > 0 || PackageWeight > 0))
                {
                    Guid productTypeId = SelectedProductType.Id;
                    Guid packingTypeId = SelectedPackingType.Id;

                    await _firebaseHelper.UpdateProductitem(
                        DTOSelectedProduct.Id,
                        packingTypeId,
                        productTypeId,                        
                        Comment,
                        PackageDate,
                        PackingDate,
                        PackageAmount,
                        Packer,
                        PackageWeight);

                    MessagingCenter.Send(App.Current, Constants.MessagingCenter.AddedProduct);

                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    // TODO: Errors
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error found: " + ex);
            }
        }
    }
}
