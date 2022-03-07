using Expense_Tracking_App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Expense_Tracking_App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void MonthlyGoalAdded_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushModalAsync(new AddMonthlyGoalPage
           {
               //binding the context with expense class

               BindingContext = new Expense()

           });
        }
    }
}
