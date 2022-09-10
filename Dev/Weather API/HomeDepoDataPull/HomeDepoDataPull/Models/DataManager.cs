using System;
using System.Net;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeDepoDataPull.Models
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
            JObject firstLocation = (JObject)jsonData["query"];
            string holdingstring;
            foreach(JProperty property in firstLocation.Properties())
            {
                holdingstring = Convert.ToString(property); 
                     
               

                   string name =Convert.ToString( firstLocation.GetValue("name"));
                    Debug.WriteLine(holdingstring);
                
            }
          // bool a = firstLocation.ContainsKey("name");
            // JObject location = (JObject)firstLocation["name"];
          
           // JObject currentWeather = (JObject)jsonData["current"];
           //Debug.WriteLine(firstLocation.ToString());
            //Debug.WriteLine(location.ToString());

           
            //weatherData.Location = firstLocation.ToString();
            //weatherData.currentTemp = int.Parse(currentWeather["temperature"].ToString());
            //weatherData.currentWeatherDescription = currentWeather["weather_descriptions"].ToString();

            return weatherData;
            
        }
    }
}

