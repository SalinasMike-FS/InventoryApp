using System;
using System.Collections.Generic;
using Xamarin.Forms;
using TruckStock.Models;
using System.IO;
using System.Globalization;
using System.Linq;
using Plugin.Messaging;

namespace TruckStock
{
    public partial class TruckInventory : ContentPage
    {
        private List<TaskData> taskList = new List<TaskData>();
        public TruckInventory()
        {
            InitializeComponent();
            addButton.Clicked += AddButton_Clicked;
            clearButton.Clicked += Clear_Clicked;
            sendEmailButton.Clicked += SendEmail_Clicked;
            DataTemplate dt = new DataTemplate(typeof(TextCell));
            dt.SetBinding(TextCell.TextProperty, new Binding("Text"));
            dt.SetBinding(TextCell.DetailProperty, new Binding("Date"));
            dt.SetValue(TextCell.TextColorProperty, Color.Blue);
            listView.ItemTemplate = dt;

            listView.ItemSelected += ListView_ItemSelected;


            MessagingCenter.Subscribe<String>(this, "ModifiedMessage", (sender) =>
            {
                this.ReloadListData();
            });

            this.ReloadListData();
        }

        private void ReloadListData()
        {
            taskList.Clear();

            //var files = Directory.EnumerateFiles(App.FolderPath, $"*.{User.cur_email}");
            var files = Directory.EnumerateFiles(App.FolderPath, $"{User.cur_email}_*");
            foreach (var filename in files)
            {
                string[] data = File.ReadAllText(filename).Split('\n');
                if(data.Length < 3)
                    continue;
                try
                {
                    DateTime.ParseExact(data[1] + " " + data[2], "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);
                }
                catch
                {
                    continue;
                }
                taskList.Add(new TaskData
                {
                    Filename = filename,
                    image = ImageSource.FromFile("event.png"),
                    Text = data[0],
                    Date = DateTime.ParseExact(data[1] + " " + data[2], "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture)
                });
            }

            listView.ItemsSource = taskList
                .OrderBy(d => d.Date)
                .ToList();
            if (taskList.Count > 0)
                clearButton.IsVisible = true;
            else
                clearButton.IsVisible = false;
        }


        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Navigation.PushAsync(new TaskEntryPage());
                MessagingCenter.Send<TaskData>((TaskData)e.SelectedItem, "EditItemMessage");
            }
        }

        async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskEntryPage
            {
                BindingContext = new TaskData()
            }); ;
        }

        async void Clear_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Clear ListView", "Are you sure you would like to delete all tasks?", "Yes", "No");

            if (answer)
            {
                if (taskList.Count > 0)
                {
                    var files = Directory.EnumerateFiles(App.FolderPath, $"{User.cur_email}_*");
                    foreach (var filename in files)
                    {
                        File.Delete(filename);
                    }
                }
                ReloadListData();
            }

        }

        private string getInventory()
        {
            string result = "";
            foreach(TaskData data in taskList)
            {
                result += data.Text + " : " + data.Date.ToString() + "\n";
            }   
            return result;
        }


        async void SendEmail_Clicked(object sender, EventArgs e)
        {
            string toemail = emailEntry.Text;
            if (string.IsNullOrEmpty(toemail))
            {
                await DisplayAlert("Warning", "Fill Email Input", "Ok");
                return;
            }
            //var emailTask = MessagingPlugin.EmailMessenger;
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                // Send simple e-mail to single receiver without attachments, bcc, cc etc.
                emailMessenger.SendEmail(toemail, "Inventory Data of " + User.cur_email, getInventory());

                await DisplayAlert("Success", "Sent Email", "Ok");
            }
        }
        
    }
}
