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
    public partial class SignIn : ContentPage
    {
        public SignIn()
        {
            InitializeComponent(); 
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {

            User newUser = new User();

            string user_email = usernameEntry.Text;  
            string user_password = passwordEntry.Text;

            if(string.IsNullOrEmpty(user_email) || string.IsNullOrEmpty(user_password))
            {
                await DisplayAlert("Invalid", "Email or Password can not be empty!", "OK");
                return;
            }

            User curUser = SavetheUser.login(user_email, user_password);
            if(curUser == null)
            {
                messageLabel.Text = "This user is not registered.";
            }
            else
            {
                User.myUser = curUser;
                await Navigation.PopAsync();
            }
            
        }
       
    }
}

