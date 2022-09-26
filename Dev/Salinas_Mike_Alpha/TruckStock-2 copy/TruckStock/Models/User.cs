using System;
using System.Collections.Generic;
using System.IO;

namespace TruckStock.Models
{
    public class User
    {
        public static User myUser;
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string UserCity { get; set; }
        public string UserState { get; set; }

        public User()
        {
            Email = "";
            Name = "";
            Password = "";
            PhoneNumber = "";
            UserCity = "";
            UserState = "";
        }

        public User(User old)
        {
            this.Name = old.Name;
            this.Email = old.Email;
            this.Password = old.Password;
            this.PhoneNumber = old.PhoneNumber;
            this.UserCity = old.UserCity;
            this.UserState = old.UserState;

        }

        public override string ToString()
        {
            return Name + ":" + Email + ":" + Password + ":" + PhoneNumber + ":" + UserCity + ":" + UserState;
        }

        public bool isValid()
        {
            if (string.IsNullOrEmpty(Name))
                return false;
            if (string.IsNullOrEmpty(Email)) 
                return false;
            if (string.IsNullOrEmpty(Password))
                return false;
            return true;
        }

    }
}

