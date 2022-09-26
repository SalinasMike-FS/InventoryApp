using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;
using TruckStock.Models;
using System.Globalization;

namespace TruckStock
{
    public partial class TaskEntryPage : ContentPage

    {
        TaskData editTask;
        public TaskEntryPage()
        {
            InitializeComponent();

            saveButton.ImageSource = ImageSource.FromFile("save48.png");
            deleteButton.ImageSource = ImageSource.FromFile("delete48.png");

            saveButton.Clicked += OnSaveButtonClicked;
            deleteButton.Clicked += DeleteButton_Clicked;

            MessagingCenter.Subscribe<TaskData>(this, "EditItemMessage", (sender) => 
            {
                editTask = sender;
                string path = sender.Filename;
                string[] strData = File.ReadAllText(path).Split('\n');

                taskEditor.Text = strData[0];
                curDate.Date = sender.Date.Date;
                curTime.Time = sender.Date.TimeOfDay;
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete Task", "Are you sure you would like to delete this?", "Yes", "No");

            Debug.WriteLine("Popup Answer: " + answer);

            if (answer)
            {
                if (editTask != null)
                {
                    if (File.Exists(editTask.Filename))
                    {
                        File.Delete(editTask.Filename);
                    }
                }
                MessagingCenter.Send<String>("Delete", "ModifiedMessage");
                await Navigation.PopAsync();
            }
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            string filename = Path.Combine(App.FolderPath, $"{User.cur_email}_{Path.GetRandomFileName()}.IPGDay3.txt");
            string messageType = "New";
            if (taskEditor.Text == "")
                return;
            if (editTask != null)
            {
                // Save
                filename = editTask.Filename;
                messageType = "Edit";
            }
            string strData = taskEditor.Text + "\n" + curDate.Date.ToString("yyyy-MM-dd") + "\n" + curTime.Time.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture);

            File.WriteAllText(filename, strData);
            MessagingCenter.Send<String>(messageType, "ModifiedMessage");
            Navigation.PopAsync();
        }
    }
}
