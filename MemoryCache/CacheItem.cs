using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMngtWS.MemoryCache
{
    public class CacheItem<T>
    {
        public T Value { get; set; }
        public DateTime TimeStamp { get; private set; }

        public CacheItem()
        {
            TimeStamp = DateTime.Now;
        }        
    }
}
