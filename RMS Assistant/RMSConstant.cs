using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMS_Assistant
{
    class RMSConstant : RMSNode
    {
        public override List<string> AllAvailableNames { get { return RMSNodeNameConstants.Constants.Keys.ToList(); } }

        public override Dictionary<string, int[]> DictNameAttributesConfigs { get { return RMSNodeNameConstants.Constants; } }

        public override string AllAttributes { get { return Attribute0 + " " + Attribute1; } }

        public RMSConstant(RMSNode parent, MainWindow ui) : base("Constant", parent, ui)
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
            file.WriteLine(indent + "#const " + AllAttributes + comment);
        }

        public override RMSNode GetRelevantParent(RMSNode caller)
        {
            return Parent.GetRelevantParent(caller);
        }

        public override RMSNode Clone()
        {
            RMSConstant clone = new RMSConstant(Parent, UI)
            {
                Attribute0 = String.Copy(Attribute0),
                Min0 = Min0,
                Max0 = Max0,
                Attribute1 = String.Copy(Attribute1),
                Min1 = Min1,
                Max1 = Max1,
                Comment = String.Copy(Comment)
            };
            return clone;
        }
    }
}
