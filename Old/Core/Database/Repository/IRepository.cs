using System.Collections.Generic;
using System.Linq;

namespace Core.Database.Repository
{
    /*
     * CREATE
     * READ
     * UPDATE
     * DELETE
     *
     * CRUD
     */
    
    public interface IRepository<T> where T : class
    {
        void Create(T obj);

        void CreateMany(IEnumerable<T> objs);

        T FindOne(IQueryable<T> query);
    }
}