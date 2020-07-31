using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_Assistant
{
    class RMSHeader : RMSNode
    {
        public RMSHeader(string name) : base(name)
        {
            parent = null;
        }

    }
}
