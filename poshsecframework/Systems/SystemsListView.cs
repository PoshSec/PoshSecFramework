using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PoshSec.Framework
{
    public class SystemsListView : ListView
    {
        public void Add(SystemsListViewItem item)
        {
            Items.Add(item);
        }

        public bool IsValid(string systemName)
        {
            var isUnique = Items.Cast<ListViewItem>().All(i => i.Text != systemName);
            return isUnique;
        }

        public void Load(List<NetworkNode> networkNodes)
        {
            // TODO: load network nodes
        }
    }
}
