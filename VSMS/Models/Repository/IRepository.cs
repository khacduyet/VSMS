using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VSMS.Models.Repository
{
    interface IRepository<T> where T : class, new()
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        T Get(object id);
        bool Add(T entity);
        bool Edit(T entity);
        bool Remove(object id);
        bool Remove(T entity);
        void Save();
    }
}
