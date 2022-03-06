using Expense_Tracking_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense_Tracking_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMonthlyGoalPage : ContentPage
    {
        public AddMonthlyGoalPage()
        {
            InitializeComponent();
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}