using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using weather.models;

namespace weather.services
{
    public class WeatherServices
    {
        private string url = "https://api.openweathermap.org/data/2.5/weather?";
        private string key = "63fd3d7bc38ab53a25ec522a6e051a80";

        public async Task<TemperatureModel> GetWeatherByGeolocation(string lat, string longt)
        {
            TemperatureModel temperature = new TemperatureModel();

            using(HttpClient client = new HttpClient())
            {
                try
                {
                    string output = await client.GetStringAsync(url + $"lat={lat}&lon={longt}&appid={key}&lang=pt&units=metric");
                    temperature = JsonConvert.DeserializeObject<TemperatureModel>(output);
                } catch(Exception ex) { }
            }

            return temperature;
        }

    }
}
