using System;
using Xamarin.Forms;

namespace TruckStock.Models
{
    public class TaskData
    {
        public string Filename { get; set; }
        public ImageSource image { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
