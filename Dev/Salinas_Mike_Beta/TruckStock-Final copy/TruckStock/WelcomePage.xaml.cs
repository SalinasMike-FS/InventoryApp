using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckStock.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TruckStock
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : TabbedPage
    {
        private User user = new User();
        private List<TaskData> taskList = new List<TaskData>();
        public WelcomePage()
        {
            InitializeComponent();

        }
    }
}
