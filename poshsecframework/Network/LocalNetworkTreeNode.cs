using System.Windows.Forms;

namespace PoshSec.Framework
{
    public class LocalNetworkTreeNode : TreeNode
    {
        public LocalNetworkTreeNode(string name) : base(name, 3, 3)
        {
            Name = name;
            Tag = NetworkType.Local;
        }
    }
}