using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Infrastructure.Realtime
{
    public abstract class RealTimeContext : IRealtimeContext
    {
        private readonly ConcurrentDictionary<Type, object> _sets = new ConcurrentDictionary<Type, object>();
        private readonly string _contextName;

        protected RealTimeContext(string contextName = null)
        {
            _contextName = contextName ?? GetType().Name;
            AutoRegisterSets();
        }

        public string ContextName => _contextName;

        public IRealtimeSet<T> Set<T>() where T : class
        {
            if (_sets.TryGetValue(typeof(T), out var set))
                return (IRealtimeSet<T>)set;

            throw new InvalidOperationException($"Set<{typeof(T).Name}> chưa được đăng ký trong context '{_contextName}'.");
        }

        public bool TryGetSet<T>(out IRealtimeSet<T> set) where T : class
        {
            if (_sets.TryGetValue(typeof(T), out var obj))
            {
                set = (IRealtimeSet<T>)obj;
                return true;
            }
            set = null;
            return false;
        }

        public bool HasSet<T>() where T : class => _sets.ContainsKey(typeof(T));

        public IEnumerable<Type> GetSetTypes() => _sets.Keys;

        public void ClearAll()
        {
            foreach (var set in _sets.Values)
            {
                var method = set.GetType().GetMethod("Clear");
                method?.Invoke(set, null);
            }
        }

        public void Reload()
        {
            _sets.Clear();
            AutoRegisterSets();
        }

        public void PrintAllSets()
        {
            Console.WriteLine($"Context '{_contextName}' has these sets:");
            foreach (var kvp in _sets)
            {
                Console.WriteLine($"- EntityType: {kvp.Key.Name}, Set Type: {kvp.Value.GetType().Name}");
            }
        }

        private void AutoRegisterSets()
        {
            var props = GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(p => p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition() == typeof(IRealtimeSet<>));

            foreach (var prop in props)
            {
                var entityType = prop.PropertyType.GetGenericArguments()[0];
                var setType = typeof(RealtimeSet<>).MakeGenericType(entityType);
                var instance = Activator.CreateInstance(setType);
                _sets[entityType] = instance;
                prop.SetValue(this, instance);
            }
        }
    }
}