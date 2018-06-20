using System.Windows.Forms;

namespace PoshSec.Framework
{
    public class DomainNetworkTreeNode : TreeNode
    {
        public DomainNetworkTreeNode(string name)
        {
            Text = name;
            SelectedImageIndex = 3;
            ImageIndex = 3;
            Tag = 2;
        }
    }
}