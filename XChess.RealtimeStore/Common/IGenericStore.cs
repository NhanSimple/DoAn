using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Store.Common
{
    public interface IGenericStore<T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        bool TryGet(string id, out T entity);
        bool TryAdd(string id, T entity);
        bool TryRemove(string id);
        bool Contains(string id);
        void Clear();
        void Update(string id, T entity);
        IEnumerable<U> GetAllOf<U>() where U : class;
    }
}
