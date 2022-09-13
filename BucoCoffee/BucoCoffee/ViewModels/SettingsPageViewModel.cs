using BucoCoffee.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucoCoffee.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        private Random random = new Random();

        public ObservableCollection<ProductType> ProductTypesList { get; set; }
        public string TypeTitle { get; set; }

        private Color _selectedColor;
        public Color SelectedColor {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
                OnPropertyChanged(nameof(SelectedColor));
            }
        }
        
        public ICommand AddProductTypeCommand => new Command(AddProductType);

        public SettingsPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();

            SelectedColor = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));
            ProductTypesList = new ObservableCollection<ProductType>(await _firebaseHelper.GetAllProductTypes());

            OnPropertyChanged(nameof(ProductTypesList));
            OnPropertyChanged(nameof(SelectedColor));
        }

        private async void AddProductType()
        {
            await _firebaseHelper.AddProductType(TypeTitle, HexConverter(SelectedColor));

            ProductTypesList = new ObservableCollection<ProductType>(await _firebaseHelper.GetAllProductTypes());
            TypeTitle = String.Empty;

            OnPropertyChanged(nameof(TypeTitle));
            OnPropertyChanged(nameof(ProductTypesList));
        }

        private static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
    }
}
