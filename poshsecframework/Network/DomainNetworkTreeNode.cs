using System.Windows.Forms;

namespace PoshSec.Framework
{
    public class DomainNetworkTreeNode : TreeNode
    {
        public DomainNetworkTreeNode(string name) : base(name, 3, 3)
        {
            Name = name;
            Tag = NetworkType.Domain;
        }
    }
}