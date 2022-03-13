using Expense_Tracking_App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense_Tracking_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddExpensePage : ContentPage
    {
        public AddExpensePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var exp = (Expense)BindingContext;
            if (!string.IsNullOrEmpty(exp.FileName))
            {
                ExpenseAmount.Text = File.ReadAllText(exp.FileName);
            }
        }
        private async void OnSave_Clicked(object sender, EventArgs e)
        {
            var exp = (Expense)BindingContext;
            if (string.IsNullOrEmpty(exp.FileName))
            {
                //Create new file
                exp.FileName = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.{ExpenseName.Text}.expense.txt");
            }
            File.WriteAllText(exp.FileName, ExpenseAmount.Text);
            await Navigation.PopModalAsync();
        }

        private void OnDelete_Clicked(object sender, EventArgs e)
        {

        }
    }
}