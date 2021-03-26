using BarrowBooks.DataAccess.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstInitializeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstInitializeWithDapper firstInitialize = new FirstInitializeWithDapper();
            firstInitialize.StartInitialize();
        }
    }
}
