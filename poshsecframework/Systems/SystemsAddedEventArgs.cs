using System.Collections;
using System.Collections.Generic;

namespace PoshSec.Framework
{
    public class SystemsAddedEventArgs
    {
        public IEnumerable<SystemsListViewItem> Items { get; }

        public SystemsAddedEventArgs(IEnumerable<SystemsListViewItem> items)
        {
            Items = items;
        }
    }
}