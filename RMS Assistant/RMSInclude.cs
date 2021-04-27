using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_Assistant
{
    class RMSInclude : RMSNode
    {

        public RMSInclude(string name, RMSNode parent, MainWindow ui) : base(name, parent, ui)
        {
            Parent = parent;
            UI = ui;
        }
        public override List<string> AllAvailableNames { get { return RMSNodeNameConstants.Includes.Keys.ToList(); } }

        public override Dictionary<string, int> DictNameNbAttributes { get { return RMSNodeNameConstants.Includes; } }


        public override string AllAttributes { get { return Attribute0; }
}

        public override RMSNode Clone()
        {
            RMSInclude clone = new RMSInclude(Name, Parent, UI)
            {
                Attribute0 = String.Copy(Attribute0),
                Comment = String.Copy(Comment)
            };
            return clone;
        }

        public override RMSNode GetRelevantParent(RMSNode caller)
        {
            return Parent.GetRelevantParent(caller);
        }

        public override void Print(StreamWriter file, uint indentLevel)
        {
            string indent = new string(' ', (int)(indentLevel * 2));
            string comment = "";
            if (Comment != "")
            {
                comment = " /* " + Comment + " */ ";
            }
            file.WriteLine(indent + Name + " " + AllAttributes + comment);
        }
    }
}
