using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weather.ViewModels;
using Xamarin.Forms;

namespace weather
{
    public partial class MainPage : ContentPage
    {
        public HomeViewModel HomeViewModel { get; set; }
        public MainPage()
        {
            InitializeComponent();

            this.HomeViewModel = new HomeViewModel();
            this.BindingContext = this.HomeViewModel;
        }

        protected override  async void OnAppearing()
        {

            bool complete = await this.HomeViewModel.GetLatLong();
            if (complete)
            {
                this.HomeViewModel.GetWeatherByGeolocation();
            }


            MessagingCenter.Subscribe<HomeViewModel>(this, "Ver", (msg) =>
            {
                this.HomeViewModel.SearchCity();
            });

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<HomeViewModel>(this, "Ver");

            base.OnDisappearing();
        }

    }
}
