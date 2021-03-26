using BarrowBooks.Entites.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrowBooks.Entites.Concrete
{
    public class Book : IEntity
    {
        public int ISBN { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }
    }
}
