using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HomeDepoDataPull.Models;

namespace HomeDepoDataPull
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            getWeatherButton.Clicked += GetweatherButton_Clicked;
        }

        async void GetweatherButton_Clicked(object sender, EventArgs e)
        {
           DataManager dataManager = new DataManager("New York");
           WeatherData newWeatherData = await dataManager.GetWeather();

           // weatherLocation.Text = "Location: " + newWeatherData.Location;
           // currentTemp.Text = "Current Temp: " + newWeatherData.currentTemp;
           // weatherDescription.Text = "Weather Description: " + newWeatherData.currentWeatherDescription;
        }
    }
}

