using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace TruckStock.Models
{
    public class DataManager
    {
        WebClient apiConnection = new WebClient();
        string startAPI = "http://api.weatherstack.com/current?access_key=3ac1dca1453a2059b56d8b7343871009&query=";
        string query { get; set; }

        string apiEndPoint
        {
            get
            {
                return startAPI + query;
            }
        }
        public DataManager(string weatherToDownload)
        {

            query = weatherToDownload;
        }

        public async Task<WeatherData> GetWeather()
        {
            string apiString = await apiConnection.DownloadStringTaskAsync(apiEndPoint);
            WeatherData weatherData = new WeatherData();
            JObject jsonData = JObject.Parse(apiString);
            JObject firstLocation = (JObject)(jsonData["location"]);
            string holdingstring;
            try
            {
                weatherData.Location = Convert.ToString(jsonData["location"]["name"]);
            }
            catch
            {

            }
            try
            {
                weatherData.currentTemp = int.Parse(Convert.ToString(jsonData["current"]["temperature"]));
            }
            catch
            {

            }
            try
            {
                List<string> states = new List<string>();
                foreach(var state in jsonData["current"]["weather_descriptions"])
                {
                    states.Add(Convert.ToString(state));
                   // weatherData.currentWeatherDescription += Convert.ToString(state) + ","
                }
                weatherData.currentWeatherDescription = String.Join(", ", states.ToArray());// Convert.ToString(jsonData["current"]["weather_descriptions"]);
            }
            catch
            {

            }
            try
            {
                weatherData.query = Convert.ToString(jsonData["request"]["query"]);
            }
            catch
            {

            }
           
            

            return weatherData;

        }
    }
}