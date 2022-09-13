using BucoCoffee.ViewModels;
using Xamarin.Forms;

namespace BucoCoffee
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel(Navigation);
        }

        private void SfButtonCalendar_Clicked(object sender, System.EventArgs e)
        {
            dummyDatePicker.Focus();
        }
    }
}
