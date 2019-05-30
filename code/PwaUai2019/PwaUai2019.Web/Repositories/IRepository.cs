using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PwaUai2019.Web.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(long id);
        T FindBy(long id, string property);
        void Add(T entity);
        void Delete(long id);
        void Edit(long id, T entity);
    }
}
