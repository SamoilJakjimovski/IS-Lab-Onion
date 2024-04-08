using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BApp.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Guid? id);
        void Insert(T entity);

        void InsertMany(List<T> entities);
        void Update(T entity);
        void Delete(T entity);
    }
}
