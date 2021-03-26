using BarrowBooks.Business.Abstract;
using BarrowBooks.DataAccess.Abstract;
using BarrowBooks.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BarrowBooks.Business.Concrete
{
    public class BookTransactionManager : IBookTransactionService
    {
        private IBookTransactionDAL _bookTransactionDAL;
        public BookTransactionManager(IBookTransactionDAL bookTransactionDAL)
        {
            _bookTransactionDAL = bookTransactionDAL;
        }

        public IEnumerable<BookTransaction> GetDailyReportData()
        {
            var sqlQuery = new StringBuilder(@"SELECT * FROM BookTransaction
                                               WHERE Status = 1 AND 
                                               GETDATE() > ReturnDate OR 
                                               (ReturnDate >= DATEADD(day, -2, GETDATE()) AND ReturnDate < GETDATE())");

            return _bookTransactionDAL.Get(sqlQuery.ToString()).ToList();
        }

        public IEnumerable<BookTransaction> GetAll()
        {
            return _bookTransactionDAL.GetAll();
        }

        public void Insert(BookTransaction t)
        {
            _bookTransactionDAL.Insert(t);
        }
    }
}
