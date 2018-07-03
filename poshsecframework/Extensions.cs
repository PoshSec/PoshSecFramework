using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshSec.Framework
{
    public static class Extensions
    {
        public static void Clear<T>(this ConcurrentBag<T> bag)
        {
            while (!bag.IsEmpty)
            {
                bag.TryTake(out T item);
            }
        }

        public static void AddRange<T>(this ConcurrentBag<T> bag, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                bag.Add(item);
            }
        }
    }
}
