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

        protected override void OnAppearing()
        {
            var budgetFile = Directory.EnumerateFiles(Environment.GetFolderPath(
                   Environment.SpecialFolder.LocalApplicationData), "*.budget.txt").FirstOrDefault();
           
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
                        Environment.SpecialFolder.LocalApplicationData), "*.exp.txt");
                var expenseList = new List<string>();
               
               
                foreach (var filename in files)
                {
                    var readFile = File.ReadAllText(filename).Split(','); ;
                    var amount = readFile.First(a => Decimal.TryParse(a,out decimal t) == true);// decimal values
                    var name = readFile.First(g => Decimal.TryParse(g, out decimal t) == false);// non decimal values
                    if(name.Length>0)
                    {

                    }
                    var exp = new Expense()
                    {
                        Amount = Decimal.Parse(amount),// the second one is amount
                        Date = File.GetCreationTime(filename),
                        FileName = filename,
                        Name = name.Length > 1? char.ToUpper(name[0]) + name.Substring(1): name,// capitlized the first letter of  expense name;
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
