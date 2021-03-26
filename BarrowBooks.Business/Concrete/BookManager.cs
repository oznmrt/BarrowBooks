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
    public class BookManager : IBookService
    {
        private IBookDAL _bookDAL;

        public BookManager(IBookDAL bookDAL)
        {
            _bookDAL = bookDAL;
        }

        public Book Get(int id)
        {
            return _bookDAL.Get(id);
        }

        public IEnumerable<Book> Get(Dictionary<string, object> keyValuePairs, string logicalOperator)
        {
            return _bookDAL.Get(keyValuePairs, logicalOperator).ToList();
        }

        public IEnumerable<Book> GetAvailableBooksBySearchFilter(Dictionary<string, object> keyValuePairs)
        {
            var sqlQuery = new StringBuilder(@"SELECT * FROM [BarrowBooks].[dbo].[Book] B 
                                               LEFT JOIN BookTransaction BT ON BT.BookISBN = B.ISBN
                                               WHERE (BT.Status = 0 OR BT.Status IS NULL) ");

            foreach (var item in keyValuePairs)
            {
                sqlQuery.Append($" AND {item.Key} LIKE @{item.Key}");
            }

            var newDictionary = keyValuePairs.ToDictionary(p => "@" + p.Key, d => (object)("%" + d.Value + "%"));

            return _bookDAL.Get(sqlQuery.ToString(), newDictionary).ToList();
        }

        public IEnumerable<Book> GetAll()
        {
            return _bookDAL.GetAll().ToList();
        }

        public void Insert(Book t)
        {
            _bookDAL.Insert(t);
        }
    }
}
