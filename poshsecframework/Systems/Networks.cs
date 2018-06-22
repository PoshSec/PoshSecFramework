using System.Collections.Generic;
using System.Linq;

namespace PoshSec.Framework
{
    public class Networks : List<Network>
    {
        public bool IsValid(string name)
        {
            return this.All(n => n.Name != name);
        }
    }
}