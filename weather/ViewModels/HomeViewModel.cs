using System;
using weather.models;
using weather.services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace weather.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        public TemperatureModel temperatureModel { get; set; }

        string _busca = "";
        public string Busca
        {
            get
            {
                return _busca;
            }
            set
            {
                _busca = value;
            }
        }


        string _temperature = "";
        public string Temperature
        {
            get
            {
                return _temperature;
            }
            set
            {
                _temperature = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Temperature));
            }
        }

        string _animation = "generic.json";
        public string Animation
        {
            get
            {
                return _animation;
            }
            set
            {
                _animation = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Animation));
            }
        }

        bool _darkMode = false;
        public bool Darkmode
        {
            get
            {
                return _darkMode;

            }
            set
            {
                _darkMode = value;
                ChangeTheme();
            }
        }

        string _theme = "#ade8f4";
        public string Theme
        {
            get
            {
                return _theme;
            }
            set
            {
                _theme = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Theme));
            }
        }


        string _lat = "";
        public string Lat
        {
            get
            {
                return _lat;
            }
            set
            {
                _lat = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Lat));
            }
        }

        string _long = "";
        public string Long
        {
            get
            {
                return _long;
            }
            set
            {
                _long = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Long));
            }
        }

        string _city = "";
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(City));
            }
        }


        public HomeViewModel()
        {
            GetLatLong();
            GetWeatherByGeolocation();
        }

        public async void GetLatLong()
        {
            Location location = await Geolocation.GetLastKnownLocationAsync();
            this.Lat = location.Latitude.ToString();
            this.Long = location.Longitude.ToString();
        }

        public async void GetWeatherByGeolocation()
        {
            temperatureModel = await new WeatherServices().GetWeatherByGeolocation(lat: this.Lat, longt: this.Long);
            this.Temperature = temperatureModel.main.temp.ToString();
            this.City = temperatureModel.name;
        }


        public void ChangeTheme()
        {
            if (this.Darkmode == true)
            {
                this.Theme = "#03071e";
            } else
            {
                this.Theme = "#ade8f4";
            }
        }

        public void ChangeAnimation()
        {
            if (this.Temperature.Equals(""))
            {
                this.Animation = "generic.json";
            }
        }
    }
}
