using Expense_Tracking_App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Expense_Tracking_App
{
    public partial class MainPage : ContentPage
    {
        Budget budget;
        public MainPage()
        {
            InitializeComponent();

        }

        protected async  override void OnAppearing()
        {

            var budgetFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.budget.txt");
            if(File.Exists(budgetFile))
            {
                AddGoal.IsVisible = false;
                GoalUpdate.IsVisible = true;
                var goal = File.ReadAllText(budgetFile);
                GoalValue.Text = goal;
                //AddExpense.IsVisible = true;

                decimal totaexp = 0m;
                var exps = new List<Expense>();
                var files = Directory.EnumerateFiles(Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData), "*.expense.txt");
                foreach (var filename in files)
                {
                    var exp = new Expense
                    {
                        Amount = Decimal.Parse(File.ReadAllText(filename)),
                        Date = File.GetCreationTime(filename),
                        FileName = filename// this is path not file name.
                    };
                    exps.Add(exp);
                }
                exps.ForEach(e => totaexp = totaexp + e.Amount);
                var balance = Decimal.Parse(goal) - totaexp;
                BalanceValu.Text = balance.ToString();
                ExpensesListView.ItemsSource = exps.OrderByDescending(n => n.Date);

            }
            else
            {

                AddGoal.IsVisible = true;
                AddExp.IsVisible = false;
                
            }

        }
        private async void MonthlyGoalAdded_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddMonthlyBudgetGoal
            {
                BindingContext = new Budget()
            }); ;
                
           
        }

        private async void AddExp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddExpensePage
            {
                BindingContext = new Expense()
            });
        }

        private async void ExpensesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new AddExpensePage
            {
                BindingContext = (Expense)e.SelectedItem
            });
        }
    }
}
