using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TruckStock.Models;

namespace TruckStock
{
    
    public partial class LandingPage : ContentPage
    {
        User cur_user;
        public LandingPage()
        {
            InitializeComponent(); 
            if (User.myUser != null)
            {
                cur_user = new User(User.myUser);
                User.myUser = null;
                user_name.Text = "Name: " + cur_user.Name; 
                user_email.Text = "Email: " + cur_user.Email;
                user_phone.Text = "PhoneNumber: " + cur_user.PhoneNumber;
               // user_password.Text = "Password: " + cur_user.Password;
                user_city.Text = "City: " + cur_user.UserCity;
                user_state.Text = "State: " + cur_user.UserState;
            }
            getWeatherButton.Clicked += GetWeatherButton_Clicked;
            
        }

        async void GetWeatherButton_Clicked(object sender, EventArgs e)
        {
            DataManager dataManager = new DataManager(cur_user.UserCity); //user's input city
            WeatherData newWeatherData = await dataManager.GetWeather();

            weatherLocation.Text = "Location: " + newWeatherData.Location;
            currentTemp.Text = "Current Temp: " + newWeatherData.currentTemp;
            weatherDescription.Text = "Weather Description: " + newWeatherData.currentWeatherDescription;
        }

        
    }
}