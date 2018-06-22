using System;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Forms;
using PoshSec.Framework.Strings;

namespace PoshSec.Framework
{
    public class NetworksTreeView : TreeView
    {
        private void Add(TreeNode network)
        {
            Nodes[0].Nodes.Add(network);
        }

        public void Load(Networks networks)
        {
            SuspendLayout();
            Nodes[0].Nodes.Clear();
            foreach (var network in networks)
            {
                var name = network.Name;
                switch (network)
                {
                    case LocalNetwork _:
                        Add(new LocalNetworkTreeNode(name));
                        break;
                    case DomainNetwork _:
                        Add(new DomainNetworkTreeNode(name));
                        break;
                }
            }
            ResumeLayout(true);
        }

        public int Count => Nodes[0].Nodes.Count;

        private class LocalNetworkTreeNode : TreeNode
        {
            public LocalNetworkTreeNode(string name) : base(name, 3, 3)
            {
                Name = name;
                Tag = NetworkType.Local;
            }
        }


        private class DomainNetworkTreeNode : TreeNode
        {
            public DomainNetworkTreeNode(string name) : base(name, 3, 3)
            {
                Name = name;
                Tag = NetworkType.Domain;
            }
        }
    }
}
