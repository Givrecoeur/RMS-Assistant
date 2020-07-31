using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_Assistant
{
    class RMSWeight : RMSNode
    {
        public RMSWeight(string name, RMSRandom parent) : base(name)
        {
            this.parent = parent;
        }

    }
}
