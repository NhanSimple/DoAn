using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Infrastructure.Realtime
{
    public interface IRealtimeContext
    {
        string ContextName { get; }
        IRealtimeSet<T> Set<T>() where T : class;
        bool TryGetSet<T>(out IRealtimeSet<T> set) where T : class;
        bool HasSet<T>() where T : class;
        IEnumerable<Type> GetSetTypes();
        void ClearAll();
        void Reload();
        void PrintAllSets();
    }
}
