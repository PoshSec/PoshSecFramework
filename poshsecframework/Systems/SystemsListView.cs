using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PoshSec.Framework
{
    public class SystemsListView : ListView
    {
        public event EventHandler<SystemsAddedEventArgs> SystemsAdded;
        private bool _loadExecuting;

        private void Add(SystemsListViewItem item)
        {
            Items.Add(item);
            if (!_loadExecuting)
                OnSystemsAdded();
        }

        public void Add(INetworkNode system)
        {
            var item = new SystemsListViewItem(system);
            Add(item);
        }

        public bool IsValid(string systemName)
        {
            var isUnique = Items.Cast<ListViewItem>().All(i => i.Text != systemName);
            return isUnique;
        }

        public void Load(IEnumerable<INetworkNode> networkNodes)
        {
            _loadExecuting = true;
            BeginUpdate();
            Items.Clear();
            foreach (var node in networkNodes)
            {
                Add(node);
            }
            EndUpdate();
            Refresh();
            OnSystemsAdded();
            _loadExecuting = false;
        }

        protected virtual void OnSystemsAdded()
        {
            SystemsAdded?.Invoke(this, new SystemsAddedEventArgs(Items.OfType<SystemsListViewItem>()));
        }
    }
}
