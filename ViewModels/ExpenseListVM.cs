using MauiApp1.Models;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels
{
    public class ExpenseListVM : INotifyPropertyChanged
    {
        ExpenseVM expense_VM;
        bool isEditing;
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly ExpenseModelContext expenseModelContext = new ExpenseModelContext();

        public bool IsEditing
        {
            private set { SetProperty(ref isEditing, value); }
            get { return isEditing; }
        }

        public ExpenseVM Expense_VM
        {
            set { SetProperty(ref expense_VM, value); }
            get { return expense_VM; }
        }

        public ICommand AddCommand { private set; get; }
        public ICommand ShowForm {  private set; get; } 

        public IList<ExpenseVM> Expenses_List { get; set; } = new ObservableCollection<ExpenseVM>();

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ExpenseListVM()
        {
            using (var expenseModelContext = new ExpenseModelContext())
            {
                var expensesFromDB = expenseModelContext.Expenses.Where(expense => expense.dateTime.Date == DateTime.Today).ToList();

                foreach (var expense in expensesFromDB)
                {
                    Expenses_List.Add(new ExpenseVM
                    {
                        Title = expense.ExpenseTitle,
                        Single = expense.ExpenseValue,
                        Date = expense.dateTime,
                        Total = expense.ExpenseValueTotal
                    });
                }
            }

            ShowForm = new Command(
            execute: () =>
            {
                Expense_VM = new ExpenseVM();   
                Expense_VM.PropertyChanged += OnExpensePropertyChanged;
                IsEditing = true;
            },
            canExecute: () =>
            {
                return !IsEditing;
            });

            void OnExpensePropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                (AddCommand as Command).ChangeCanExecute();
            }

            void RefreshCanExecutes()
            {
                (AddCommand as Command).ChangeCanExecute();
            }

            AddCommand = new Command(execute: () =>
            {
                Expenses_List.Add(Expense_VM);
                Expense_VM.Total += Expense_VM.Single;
                Expense_VM.PropertyChanged -= OnExpensePropertyChanged;
                IsEditing = false;
                RefreshCanExecutes();

                var expense = new ExpenseModel
                {
                    dateTime = DateTime.Now,
                    ExpenseTitle = Expense_VM.Title,
                    ExpenseValue = Expense_VM.Single,
                    ExpenseValueTotal = Expense_VM.Total
                };

                expenseModelContext.Expenses.Add(expense);
                expenseModelContext.SaveChanges();


            }, canExecute: () =>
            {
                return Expense_VM != null &&
                       Expense_VM.Title != null &&
                       Expense_VM.Single > 0;
            });

        }    
    }
}
