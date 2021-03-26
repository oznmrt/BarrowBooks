using BarrowBooks.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarrowBooks.UI.ViewModels
{
    public class DailyReportViewModel
    {
        public BookTransaction bookTransaction { get; set; }

        public decimal penaltyAmount { get; set; }
    }
}