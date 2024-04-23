using MauiApp1.Models;
using MauiApp1.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Maui.Controls.Platform;


namespace MauiApp1.Views
{
    public partial class MainPage : ContentPage
    {
        private bool WasClicked = true;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ExpenseListVM();   
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (WasClicked)
            {
                ExpensesListText.TranslateTo(0, 0, 1500);
                expensesList.TranslateTo(0, 0, 1500);
                await Expense_Title.FadeTo(1, 500);
                await Expense_Cost.FadeTo(1, 500);
                await AddBtn.FadeTo(1, 500);
                
                WasClicked = false;
            }else{
                ExpensesListText.TranslateTo(0, -200, 1500);
                expensesList.TranslateTo(0, -200, 1500);
                await AddBtn.FadeTo(0, 500);
                await Expense_Cost.FadeTo(0, 500);
                await Expense_Title.FadeTo(0, 500);

                WasClicked = true;   
            }
        }
    }

}
