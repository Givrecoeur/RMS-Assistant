using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_Assistant
{
    class RMSAttribute : RMSNode
    {
        public RMSAttribute(string name, RMSNode parent) : base(name)
        {
            this.parent = parent;
        }
    }
}
