using System.Windows.Forms;

namespace PoshSec.Framework
{
    public class LocalNetworkTreeNode : TreeNode
    {
        public LocalNetworkTreeNode(string name)
        {
            Text = name;
            SelectedImageIndex = 3;
            ImageIndex = 3;
            Tag = 1;
        }
    }
}