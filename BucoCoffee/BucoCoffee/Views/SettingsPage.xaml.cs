using BucoCoffee.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BucoCoffee.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            BindingContext = new SettingsPageViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            (BindingContext as SettingsPageViewModel).OnAppearing();
        }

        private void HSLSliders_SelectedColorChanged(object sender, ColorPicker.BaseClasses.ColorPickerEventArgs.ColorChangedEventArgs e)
        {
            (BindingContext as SettingsPageViewModel).SelectedColor = e.NewColor;
        }
    }
}