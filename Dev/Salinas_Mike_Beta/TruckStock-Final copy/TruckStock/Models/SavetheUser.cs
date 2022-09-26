using System;
using System.Collections.Generic;
using System.IO;

namespace TruckStock.Models
{
    public class SavetheUser
    {
        private static User savingUser = new User();
        private static readonly string fileNameForSaving = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "userInfo");
        private static bool loaded = false;
        private static List<User> savedItems = new List<User>();
        public static void Save(User myuser)   //saving a list of strings into a the created file
        {
            //string curPath = Path.Combine(fileNameForSaving, myuser.Email);
            using (StreamWriter sw = File.AppendText(fileNameForSaving))
            {
                sw.WriteLine(myuser);
            }
            savedItems.Add(myuser);            
        }

        public static void Load()   //This checks to see if theres a file and then loads the strings. adding the check prevented the error
        {            
            loaded = true;            
            if (!File.Exists(fileNameForSaving))
                return;
            string[] loadlines = File.ReadAllLines(fileNameForSaving);
            foreach(string line in loadlines)
            {
                User user = new User();
                string[] rowarr = line.Split(':');
                if (rowarr.Length != 6)
                    continue;
                user.Name = rowarr[0];
                user.Email = rowarr[1];
                user.Password = rowarr[2];
                user.PhoneNumber = rowarr[3];
                user.UserCity = rowarr[4];
                user.UserState = rowarr[5];
                savedItems.Add(user);
            }            
        }

        public static bool checkConflict(User newuser)
        {
            if (!loaded)
                Load();
            foreach (User user in savedItems)
            {
                if (user.Email.Equals(newuser.Email))
                {
                    return true;
                }
            }
            return false;
        }

        public static User login(string email, string password)
        {
            if (!loaded)
                Load();
            foreach (User user in savedItems)
            {
                if (user.Email.Equals(email) && user.Password.Equals(password) )
                {
                    return user; 
                }
            }
            return null;
        }

    }
}
