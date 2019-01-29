using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamMngtWS.MemoryCache
{
    public static class MemRepository<T>
    {
        public static readonly Dictionary<string, CacheItem<T>> Repository = new Dictionary<string, CacheItem<T>>();

        public static void Clear()
        {
            Repository.Clear();
        }

        public static string Print()
        {
            StringBuilder content = new StringBuilder();

            //content.Append("\"cache\": [");
            foreach (CacheItem<T> item in MemRepository<T>.Repository.Values)
            {
                content.Append($"{{\"Time stamp\": \"{item.TimeStamp}\", \"item\": {item.Value}}},");
            }

            // remove the last comma
            if (content.Length > 0)
                content.Remove(content.Length - 1, 1);

            return content.ToString();
        }
    }
}
