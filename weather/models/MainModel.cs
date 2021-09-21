using System;
namespace weather.models
{
    public class MainModel
    {
        public float temp { get; set; }

        public float feels_like { get; set; }

        public float temp_min { get; set; }

        public float temp_max { get; set; }

        public long preassure { get; set; }

        public int humidity { get; set; }
    }
}
