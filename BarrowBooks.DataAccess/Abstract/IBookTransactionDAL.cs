using BarrowBooks.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrowBooks.DataAccess.Abstract
{
    public interface IBookTransactionDAL : IEntityRepository<BookTransaction>
    {
    }
}
