using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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


        string _minimal = "";
        public string Minimal
        {
            get
            {
                return _minimal;
            }
            set
            {
                _minimal = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Minimal));
            }
        }

        string _maximum = "";
        public string Maximum
        {
            get
            {
                return _maximum;
            }
            set
            {
                _maximum = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Maximum));
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


        bool _busy = true;
        public bool Busy
        {
            get
            {
                return _busy;
            }
            set
            {
                _busy = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Busy));
            }
        }

        public ICommand SearchCommand { get; set; }


        public HomeViewModel()
        {
            SearchCommand = new Command(() =>
            {
                MessagingCenter.Send(this, "Ver");
            });
        }

        public async Task<bool> GetLatLong()
        {
            bool complete = true;
            try
            {
                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(10));
                Location position = await Geolocation.GetLocationAsync(request);

                this.Lat = position.Latitude.ToString();
                this.Long = position.Longitude.ToString();
            } catch(Exception ex)
            {
                complete = false;
            }

            return complete;
        }

        public async void GetWeatherByGeolocation()
        {
            temperatureModel = await new WeatherServices().GetWeatherByGeolocation(lat: this.Lat, longt: this.Long);
            this.Temperature = temperatureModel.main.temp.ToString();
            this.City = temperatureModel.name;
            this.Maximum = temperatureModel.main.temp_max.ToString();
            this.Minimal = temperatureModel.main.temp_min.ToString();
            this.Animation = Lottie(description: temperatureModel.weather.FirstOrDefault().description);
            this.Busy = false;
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


        public string Lottie(string description)
        {
            string response = "generic.json";

            if (description.Contains("nuvens") || description.Contains("nuvens"))
            {
                response = "cloud.json";
            }

            if (description.Contains("céu limpo"))
            {
                response = "sunny.json";
            }

            if (description.Contains("chuva"))
            {
                response = "rain.json";
            }


            return response;
        }


        public async void SearchCity()
        {
            this.Busy = true;
            temperatureModel = await new WeatherServices().GetWeatherByCity(this.Busca);
            if (!string.IsNullOrWhiteSpace(temperatureModel.name))
            {
                this.Temperature = temperatureModel.main.temp.ToString();
                this.City = temperatureModel.name;
                this.Maximum = temperatureModel.main.temp_max.ToString();
                this.Minimal = temperatureModel.main.temp_min.ToString();
                this.Animation = Lottie(description: temperatureModel.weather.FirstOrDefault().description);
                this.Busy = false;
            }
        }

    }
}
