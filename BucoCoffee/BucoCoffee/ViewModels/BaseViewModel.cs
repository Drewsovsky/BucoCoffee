using BucoCoffee.Helper;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace BucoCoffee.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public readonly FirebaseHelper _firebaseHelper = new FirebaseHelper();

        protected BaseViewModel() { }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void OnAppearing() { }
        public virtual void OnDisappearing() { }

        //public virtual void OnAppearing(Page sender, EventArgs eventArgs) { }
        //public virtual void OnDisappearing(Page sender, EventArgs eventArgs) { }
    }
}