using System;
using System.Collections.Generic;

namespace weather.models
{
    public class TemperatureModel
    {

        public List<WeatherModel> weather { get; set; }

        public MainModel main { get; set; }

        public string name { get; set; }


        public TemperatureModel()
        {
            main = new MainModel();
            weather = new List<WeatherModel>();
        }
    }
}
