using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarrowBooks.UI.ViewModels
{
    public class SearchBookViewModel
    {
        public int ISBN { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }
    }
}