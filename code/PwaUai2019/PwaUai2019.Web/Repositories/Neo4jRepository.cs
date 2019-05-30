using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NetGain;

namespace PwaUai2019.Web.Repositories
{
    public class Neo4jRepository<T> : IRepository<T> where T : class
    {
        internal NodeProvider _nodeProvider;

        public Neo4jRepository(NodeProvider nodeProvider)
        {
            _nodeProvider = nodeProvider;
        }

        public void Add(T entity)
        {
            _nodeProvider.Create(entity);
        }

        public void Delete(long id)
        {
            _nodeProvider.Delete(id);
        }

        public void Edit(long id, T entity)
        {
            _nodeProvider.Update(id, entity);
        }

        public T FindBy(long id, string property)
        {
            object result = new object();
            result = _nodeProvider.Get(id, property);
            return (T)result;
        }

        public T Get(long id)
        {
            object result = new object();
            result = _nodeProvider.Get(id);
            return (T)result;
        }
    }
}
