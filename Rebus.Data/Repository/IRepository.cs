using System.Collections.Generic;
using System.Linq;

namespace Rebus.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        void Create(T obj);

        void CreateMany(IEnumerable<T> objs);

        T FindOne(IQueryable<T> query);
    }
}