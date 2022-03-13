using Expense_Tracking_App.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense_Tracking_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMonthlyBudgetGoal : ContentPage
    {
        private decimal goal = 0;
        public AddMonthlyBudgetGoal()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
           
        }
        private async void OnSave_Clicked(object sender, EventArgs e)
        {
           
            var budget = (Budget)BindingContext;
            if (string.IsNullOrEmpty(budget.Filename))
            {
                // create new file
                budget.Filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.budget.txt");
            }
            
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
            if (Decimal.TryParse(BudgetEditor.Text, out goal))
            {
                File.WriteAllText(budget.Filename, BudgetEditor.Text);
            }
            else
            {
                // TO DO: Add error message to the user to tell this is not legal decimal.
            }
            await Navigation.PopAsync();
            

        }

        private void OnDelete_Clicked(object sender, EventArgs e)
        {
            var budget = (Budget)BindingContext;
            if(File.Exists(budget.Filename))
            {
                File.Delete(budget.Filename);
            }
            BudgetEditor.Text = String.Empty;
        }
    }
}