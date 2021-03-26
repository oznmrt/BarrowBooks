using BarrowBooks.Entites.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BarrowBooks.DataAccess.Abstract
{
    public interface IEntityRepository<TEntity> where TEntity: class, IEntity, new()
    {
        IEnumerable<TEntity> GetAll();
        void DeleteRow(int id);
        TEntity Get(int id);
        IEnumerable<TEntity> Get(Dictionary<string, object> keyValuePairs, string logicalOperator);
        IEnumerable<TEntity> Get(string sqlQuery);
        IEnumerable<TEntity> Get(string sqlQuery, Dictionary<string, object> parameters);
        int SaveRange(IEnumerable<TEntity> list);
        void Update(TEntity t);
        void Insert(TEntity t);
    }
}
