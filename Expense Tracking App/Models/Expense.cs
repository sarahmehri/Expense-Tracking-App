﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracking_App.Models
{
   public class Expense
    {
        

        public string Name { get; set; }
        public string FileName { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
