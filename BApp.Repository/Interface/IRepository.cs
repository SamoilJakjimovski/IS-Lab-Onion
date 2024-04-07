using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BApp.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Guid? id);
        void Insert(T entity);

        void InsertMany(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
    }
}
