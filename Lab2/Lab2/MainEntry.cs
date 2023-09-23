using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public delegate void WeatherUpdated();
    class WeatherData
    {
        protected float _temp;
        protected float _humidity;
        protected string _location;
        protected event WeatherUpdated _stations;
        #region Properties
        public WeatherUpdated WearherStations
        {
            get { return _stations; }
            set { _stations = value; }
        }
        public float KelvinDegrees
        {
            get { return _temp; }
            set
            {
                _temp = value;
                foreach (WeatherUpdated s in _stations.GetInvocationList()) s();
            }
        }
        public float Humidity
        {
            get { return _humidity; }
            set
            {
                _humidity = value;
                foreach (WeatherUpdated s in _stations.GetInvocationList()) s();
            }
        }
        public string Location
        {
            get { return _location; }
        }
        #endregion
        public WeatherData(string location, float temp, float humility)
        {
            _location = location;
            _temp = temp;
            _humidity = humility;
        }
    }
    abstract class WeatherStation
    {
        protected WeatherData _weather;
        protected abstract void showWeather();
    }
    class TaiwanWeatherStation : WeatherStation
    {
        public TaiwanWeatherStation(WeatherData weather)
        {
            _weather = weather;
            _weather.WearherStations += new WeatherUpdated(showWeather);
        }
        protected override void showWeather()
        {
            System.Console.WriteLine("Taiwan Weather Station:");
            System.Console.WriteLine("The weather at " + _weather.Location);
            System.Console.Write("Temperature: " + (_weather.KelvinDegrees - 273) + "C, ");
            System.Console.WriteLine("Humidity: " + _weather.Humidity + "%\n");
        }
    }
    class NewYorkWeatherStation : WeatherStation
    {
        public NewYorkWeatherStation(WeatherData weather)
        {
            _weather = weather;
            _weather.WearherStations += new WeatherUpdated(showWeather);
        }
        protected override void showWeather()
        {
            System.Console.WriteLine("New York Weather Station:");
            System.Console.WriteLine("The weather at " + _weather.Location);
            System.Console.Write("Temperature:" + (((_weather.KelvinDegrees - 273) * 9 / 5) + 32)
            + "F, ");
            System.Console.WriteLine("Humidity: " + _weather.Humidity + "%\n");
            if ((_weather.KelvinDegrees - 273) <= 0 && _weather.Humidity > 50)
                System.Console.WriteLine("It may start to snow!");
        }
    }
    public class MainEntry
    {
        public static void Main()
        {
            WeatherData weather = new WeatherData("Los Angeles", 275.0f, 40.0f);
            TaiwanWeatherStation taiwan = new TaiwanWeatherStation(weather);
            NewYorkWeatherStation newYork = new NewYorkWeatherStation(weather);
            weather.KelvinDegrees = 274.0f; weather.KelvinDegrees = 272.0f;
            weather.Humidity = 60.0f;
            System.Console.Read();
        }
    }
}