using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Infrastructure.Realtime;
namespace XChess.Store.Common
{
    public class GenericStore<T>:IGenericStore<T> where T : class
    {
        protected readonly IRealtimeContext _context;
        protected readonly IRealtimeSet<T> _set;

        public GenericStore(IRealtimeContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _set.GetAll().Select(kv => kv.Value);
        }

        public T GetById(string id)
        {
            return _set.TryGet(id, out var value) ? value : null;
        }

        public bool TryAdd(string id, T entity)
        {
            return _set.TryAdd(id, entity);
        }

        public bool TryRemove(string id)
        {
            return _set.TryRemove(id, out _);
        }

        public bool Contains(string id)
        {
            return _set.ContainsKey(id);
        }

        public void Clear()
        {
            _set.Clear();
        }

   
        public void Update(string id, T entity)
        {
            _set.TryAdd(id, entity); //ghi đè thẳng
        }
        public IEnumerable<U> GetAllOf<U>() where U : class
        {
            return _context.Set<U>().GetAll().Select(e => e.Value);
        }
        public bool TryGet(string id, out T entity)
        {
            return _set.TryGet(id, out entity);
        }
    }
}
