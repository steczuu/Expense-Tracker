using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MauiApp1.Models
{
    [PrimaryKey(nameof(Id))]
    public class ExpenseModel
    {
        public int Id { get; set; } 
        public string ExpenseTitle { get; set; }
        public float ExpenseValue { get; set; }
        public float ExpenseValueTotal { get; set; }
        public DateTime dateTime { get; set; }  
    }
}
