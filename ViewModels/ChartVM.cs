using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using MauiApp1.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace MauiApp1.ViewModels
{
    public class ChartVM : ObservableObject,INotifyPropertyChanged
    {
        Chart _chart;
        public Chart Chart
        {
            get => _chart;
            set => SetProperty(ref _chart, value);
        }

        public ICommand Refresh { get; }
        public ChartVM()
        {
            List<float> expenses = GetExpensesForLast7Days();

            List<ChartEntry> entries = new List<ChartEntry>();
            for (int i = 0; i < expenses.Count; i++)
            {
                entries.Add(new ChartEntry(expenses[i])
                {
                    Label = DateTime.Now.Date.AddDays(-i).ToString("dd/MM/yyyy"),
                    ValueLabel = expenses[i].ToString("C"),
                    Color = SKColor.Parse("#266489")
                });
            }

            Refresh = new Command(execute:  () =>
            {
                RefreshChart();
            },
            canExecute: () =>
            {
                return true;
            });


            Chart = new BarChart()
            {
                Entries = (IEnumerable<ChartEntry>)entries,
                LabelTextSize = 35,
                LabelOrientation = Orientation.Horizontal,
                BackgroundColor = SKColor.Parse("#f0f0f0"),
                LabelColor = SKColor.Parse("#266489")
            };
        }

        public void RefreshChart()
        {
            List<float> expenses = GetExpensesForLast7Days();

            List<ChartEntry> entries = new List<ChartEntry>();
            for (int i = 0; i < expenses.Count; i++)
            {
                entries.Add(new ChartEntry(expenses[i])
                {
                    Label = DateTime.Now.Date.AddDays(-i).ToString("dd/MM/yyyy"),
                    ValueLabel = expenses[i].ToString("C"),
                    Color = SKColor.Parse("#266489")
                });
            }

            Chart = new BarChart()
            {
                Entries = entries,
                LabelTextSize = 35,
                LabelOrientation = Orientation.Horizontal,
                BackgroundColor = SKColor.Parse("#f0f0f0"),
                LabelColor = SKColor.Parse("#266489")
            };
        }

        public List<float> GetExpensesForLast7Days()
        {
            List<float> expenses = new List<float>();
            using (var expenseModelContext = new ExpenseModelContext())
            {
                for (int i = 0; i < 7; i++)
                {
                    DateTime date = DateTime.Now.Date.AddDays(-i);
                    float totalExpenses = 0;

                 
                    var expensesFromDB = expenseModelContext.Expenses
                        .Where(expense => expense.dateTime.Date == date)
                        .ToList();

                    foreach (var expense in expensesFromDB)
                    {
                        totalExpenses += expense.ExpenseValue;
                    }

                    expenses.Add(totalExpenses);
                }
            }
            return expenses;
        }

    }
}
