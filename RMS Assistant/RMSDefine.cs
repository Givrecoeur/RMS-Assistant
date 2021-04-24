using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMS_Assistant
{
    class RMSDefine : RMSNode
    {
        public override List<string> AllAvailableNames { get { return RMSNodeNameConstants.Defines.Keys.ToList(); } }

        public override Dictionary<string, int> DictNameNbAttributes { get { return RMSNodeNameConstants.Defines; } }

        public override string AllAttributes { get { return Attribute0; } }

        public RMSDefine(RMSNode parent, MainWindow ui) : base("Define", parent, ui)
        {
            Parent = parent;
            UI = ui;
        }

        public override void Print(StreamWriter file, uint indentLevel)
        {
            string indent = new string(' ', (int)(indentLevel * 2));
            string comment = "";
            if (Comment != "")
            {
                comment = " /* " + Comment + " */ ";
            }
            file.WriteLine(indent + "#define " + AllAttributes + comment);
        }

        public override RMSNode GetRelevantParent(RMSNode caller)
        {
            return Parent.GetRelevantParent(caller);
        }

        public override RMSNode Clone()
        {
            RMSDefine clone = new RMSDefine(Parent, UI)
            {
                Attribute0 = String.Copy(Attribute0),
                Comment = String.Copy(Comment)
            };
            return clone;
        }
    }
}
