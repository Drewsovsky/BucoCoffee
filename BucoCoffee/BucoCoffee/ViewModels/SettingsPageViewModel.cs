using BucoCoffee.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucoCoffee.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        private Random random = new Random();

        public ObservableCollection<ProductType> ProductTypesList { get; set; }
        public ObservableCollection<PackingType> PackingTypesList { get; set; }

        private string _typeTitle;
        public string TypeTitle
        {
            get { return _typeTitle; }
            set
            {
                _typeTitle = value;
                
                if (IsProductTypeAdded)
                {
                    IsTypeTitleEmpty = false;
                    IsProductTypeAdded = false;
                }
                else
                {
                    IsTypeTitleEmpty = string.IsNullOrWhiteSpace(_typeTitle);
                }

                OnPropertyChanged(nameof(IsTypeTitleEmpty));
            }
        }

        private string _packingTypeTitle;

        public string PackingTypeTitle
        {
            get { return _packingTypeTitle; }
            set { 
                _packingTypeTitle = value;

                if (IsPackingTypeAdded)
                {
                    IsPackingTypeEmpty = false;
                    IsPackingTypeAdded = false;
                }
                else
                {
                    IsPackingTypeEmpty = string.IsNullOrWhiteSpace(_packingTypeTitle);
                }

                OnPropertyChanged(nameof(IsPackingTypeEmpty));
            }
        }

        private Color _selectedColor;
        public Color SelectedColor {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
                OnPropertyChanged(nameof(SelectedColor));
            }
        }
        public bool IsTypeTitleEmpty { get; set; } = false;
        public bool IsPackingTypeEmpty { get; set; } = false;
        private bool IsProductTypeAdded { get; set; }
        private bool IsPackingTypeAdded { get; set; }

        public ICommand AddProductTypeCommand => new Command(AddProductType);
        public ICommand AddPackingTypeCommand => new Command(AddPackingType);

        public SettingsPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();

            SelectedColor = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));
            ProductTypesList = new ObservableCollection<ProductType>(await _firebaseHelper.GetAllProductTypes());
            PackingTypesList = new ObservableCollection<PackingType>(await _firebaseHelper.GetAllPackingTypes());

            OnPropertyChanged(nameof(PackingTypesList));
            OnPropertyChanged(nameof(ProductTypesList));
            OnPropertyChanged(nameof(SelectedColor));
        }

        private async void AddProductType()
        {
            IsTypeTitleEmpty = false;

            if (string.IsNullOrWhiteSpace(TypeTitle))
            {
                IsTypeTitleEmpty = true;
            }
            else
            {
                IsTypeTitleEmpty = false;

                await _firebaseHelper.AddProductType(TypeTitle, HexConverter(SelectedColor));

                ProductTypesList = new ObservableCollection<ProductType>(await _firebaseHelper.GetAllProductTypes());
                TypeTitle = String.Empty;
                IsProductTypeAdded = true;
            }

            OnPropertyChanged(nameof(IsTypeTitleEmpty));
            OnPropertyChanged(nameof(TypeTitle));
            OnPropertyChanged(nameof(ProductTypesList));
        }

        private async void AddPackingType()
        {
            IsPackingTypeEmpty = false;

            if (string.IsNullOrWhiteSpace(PackingTypeTitle))
            {
                IsPackingTypeEmpty = true;
            }
            else
            {
                IsPackingTypeEmpty = false;

                await _firebaseHelper.AddPackingType(PackingTypeTitle);

                PackingTypesList = new ObservableCollection<PackingType>(await _firebaseHelper.GetAllPackingTypes());
                PackingTypeTitle = String.Empty;
                IsPackingTypeAdded = true;
            }

            OnPropertyChanged(nameof(IsPackingTypeEmpty));
            OnPropertyChanged(nameof(PackingTypeTitle));
            OnPropertyChanged(nameof(PackingTypesList));
        }

        private static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
    }
}
