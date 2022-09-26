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
    public partial class SignUp : ContentPage
    {
        private bool correct;

        public SignUp()
        {
            InitializeComponent(); 

            saveButton.Clicked += SaveButton_Clicked;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {

            User newUser = new User();

            newUser.Name = UserFirstName.Text + " " + UserLastName.Text;  
            newUser.Email = UserEmail.Text;
            newUser.UserCity = UserCity.Text;
            newUser.UserState = UserState.Text;
            newUser.PhoneNumber = UserPhoneNumber.Text;

            VerifyPassword(UserPasswordVerify.Text);

            if (correct != false)
            {
                
                newUser.Password = UserPasswordVerify.Text;
                if (!newUser.isValid())
                {
                    await DisplayAlert("Invalid", "Name or Email or Password can not be empty!", "OK");
                    return;
                }
                if (SavetheUser.checkConflict(newUser))
                {
                    await DisplayAlert("Conflict", "Current email already exist in old users!", "OK");
                    return;
                }
                SavetheUser.Save(newUser);
                User.myUser = newUser;
                await Navigation.PopAsync();
                
            }
        }

       async void VerifyPassword(string password)
        {
          
            if (UserPassword.Text != UserPasswordVerify.Text)
            {
                bool answer = await DisplayAlert("Failed", "Passwords do not match! Saved failed!", "Ok", "Cancel");

               correct = false;
                
            }else
            {
                correct = true;
            }

        }
    }
}

