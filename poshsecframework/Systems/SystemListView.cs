using System.Windows.Forms;

namespace PoshSec.Framework
{
    public class SystemListView : ListView
    {
        public void Add(SystemListViewItem item)
        {
            Items.Add(item);
        }
    }
}
