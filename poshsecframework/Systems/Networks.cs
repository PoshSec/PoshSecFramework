using System;
using System.Collections.Generic;
using System.Linq;

namespace PoshSec.Framework
{
    public class Networks : List<Network>
    {
        private Network _currentNetwork;

        public Networks()
        {
            _currentNetwork = Network.Empty;    
        }

        public event EventHandler<CurrentNetworkChangedEventArgs> CurrentNetworkChanged;

        public Network CurrentNetwork
        {
            get => _currentNetwork;
            set
            {
                _currentNetwork = value ?? Network.Empty;
                OnCurrentNetworkChanged(_currentNetwork);

            }
        }

        private void OnCurrentNetworkChanged(Network network)
        {
            var handler = CurrentNetworkChanged;
            handler?.Invoke(this, new CurrentNetworkChangedEventArgs(network));
        }

        public bool IsValid(string name)
        {
            return this.All(n => n.Name != name);
        }
    }
}