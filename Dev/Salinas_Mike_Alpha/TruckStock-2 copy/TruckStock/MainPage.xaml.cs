using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TruckStock.Models;

namespace TruckStock
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            signUpButton.Clicked += SignUpButton_Clicked; 
            signInButton.Clicked += SignInButton_Clicked;
        }

        async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }

        async void SignInButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignIn());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (User.myUser != null)
            {
                Navigation.PushAsync(new LandingPage());
            }
                //Console.WriteLine("Hey, Im coming to your screen");
        }
    }
}

