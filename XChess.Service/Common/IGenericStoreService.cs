using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Service.Common
{
    public interface IGenericStoreService<T> :IService where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        bool Exists(string id);
        bool TryAdd(string id, T entity);
        bool TryRemove(string id);
        void Clear();
    }
}
