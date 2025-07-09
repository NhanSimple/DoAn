using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Infrastructure.Realtime
{
    public class RealtimeSet<T> : IRealtimeSet<T>
    {
        private readonly ConcurrentDictionary<string, T> _dictionary = new ConcurrentDictionary<string, T>();

        public bool TryAdd(string key, T value)
        {
            return _dictionary.TryAdd(key, value);
        }

        public bool TryRemove(string key, out T removed)
        {
            return _dictionary.TryRemove(key, out removed);
        }

        public bool TryGet(string key, out T value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public IEnumerable<KeyValuePair<string, T>> GetAll()
        {
            return _dictionary.ToArray();
        }

        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }
    }
}
