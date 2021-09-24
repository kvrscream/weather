using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weather.ViewModels;
using weather.Views;
using Xamarin.CommunityToolkit.Extensions;
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

            MessagingCenter.Subscribe<HomeViewModel>(this, "openLoad", (msg) =>
            {
                Navigation.ShowPopup(new LoadPage());
            });

            MessagingCenter.Subscribe<HomeViewModel>(this, "openLoad", (msg) =>
            {
                new LoadPage().DimissLoad();
            });

            MessagingCenter.Subscribe<HomeViewModel>(this, "Ver", (msg) =>
            {
                this.HomeViewModel.SearchCity();
            });

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<HomeViewModel>(this, "openLoad");
            MessagingCenter.Unsubscribe<HomeViewModel>(this, "closeLoad");
            MessagingCenter.Unsubscribe<HomeViewModel>(this, "Ver");

            base.OnDisappearing();
        }

    }
}
