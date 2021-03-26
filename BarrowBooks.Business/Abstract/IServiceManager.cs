using BarrowBooks.Entites.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BarrowBooks.Business.Abstract
{
    public interface IServiceManager<TEntity> where TEntity : class, IEntity, new()
    {
        IEnumerable<TEntity> GetAll();
        void DeleteRow(int id);
        TEntity Get(int id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, object>> expression);
        int SaveRange(IEnumerable<TEntity> list);
        void Update(TEntity t);
        void Insert(TEntity t);
    }
}
