using BarrowBooks.DataAccess.Abstract;
using BarrowBooks.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrowBooks.DataAccess.Concrete.Dapper
{
    public class BookDAL : EntityRepositoryBase<Book>, IBookDAL
    {
        public BookDAL() : base("Book") { }
    }
}
