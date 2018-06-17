using System.Windows.Forms;

namespace PoshSec.Framework
{
    public class NetworksTreeView : TreeView
    {
        public bool IsValid(string networkname)
        {
            var rtn = true;
            var idx = 0;
            var rootnode = Nodes[0];
            while (idx < rootnode.Nodes.Count && rtn)
            {
                var node = rootnode.Nodes[idx];
                if (node.Text == networkname) rtn = false;
                idx++;
            }

            return rtn;
        }

        public void Add(string networkName, SystemType systemType)
        {
            var lnode = new TreeNode
            {
                Text = networkName,
                SelectedImageIndex = 3,
                ImageIndex = 3,
                Tag = systemType
            };
            Nodes[0].Nodes.Add(lnode);
        }
    }
}
