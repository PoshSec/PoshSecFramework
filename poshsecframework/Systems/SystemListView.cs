using System;
using System.Linq;
using System.Windows.Forms;

namespace PoshSec.Framework
{
    public class SystemListView : ListView
    {
        public void Add(SystemListViewItem item)
        {
            Items.Add(item);
        }

        public bool IsValid(string systemName)
        {
            var isUnique = Items.Cast<ListViewItem>().All(i => i.Text != systemName);
            return isUnique;
        }
    }
}
