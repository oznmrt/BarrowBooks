using BarrowBooks.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarrowBooks.UI.ViewModels
{
    public class CreateBookTransactionViewModel
    {
        public SelectList Members { get; set; }

        public BookTransaction TransactionData { get; set; }
    }
}