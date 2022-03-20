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
                    $"{Path.GetRandomFileName()}.{ExpenseName.Text}.exp.txt");
            }


            var content = $"{ExpenseName.Text},{ExpenseAmount.Text}";
            File.WriteAllText(exp.FileName, content);
           // File.AppendAllText(exp.FileName, ExpenseName.Text);
            
            
            
            await Navigation.PopModalAsync();
        }

        private async void OnDelete_Clicked(object sender, EventArgs e)
        {
            var exp = (Expense)BindingContext;
            if (File.Exists(exp.FileName))
            {
                File.Delete(exp.FileName);
            }
            ExpenseName.Text = string.Empty;
            await Navigation.PopModalAsync();
        }
    }
}