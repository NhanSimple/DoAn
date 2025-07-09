using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Infrastructure.Realtime
{
    public interface IRealtimeSet<T>
    {
        bool TryAdd(string key, T value);
        bool TryRemove(string key, out T removed);
        bool TryGet(string key, out T value);
        IEnumerable<KeyValuePair<string, T>> GetAll();
        bool ContainsKey(string key);
        void Clear();
    }
}
