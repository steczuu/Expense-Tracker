using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MauiApp1.ViewModels
{
    public class ExpenseVM : INotifyPropertyChanged
    {
        float total;
        float single;
        DateTime date = DateTime.Now;
        string title;

        public event PropertyChangedEventHandler PropertyChanged;

        public float Total
        {
            set { SetProperty(ref total,value); }
            get { return total; }
        }

        public float Single
        {
            set { SetProperty(ref single, value); }
            get { return single; }
        }

        public string Title
        {
            set { SetProperty(ref title, value); }
            get { return title; }
        }

        public DateTime Date
        {
            set { SetProperty(ref date, value); }
            get { return date; }
        }    

        public override string ToString()
        {
            return "Title: " + Title + " |" + " Cost: " + Single;
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if(Object.Equals(storage, value)) return false;

            storage = value;    
            OnPropertyChanged(propertyName);
            return true;    
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
