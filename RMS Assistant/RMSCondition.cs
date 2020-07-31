using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMS_Assistant
{
    class RMSCondition : RMSNode
    {
        public override List<string> AllAvailableNames { get { return RMSNodeNameConstants.Conditions.Keys.ToList(); } }

        public override Dictionary<string, int[]> DictNameAttributesConfigs { get { return RMSNodeNameConstants.Conditions; } }

        public override string AllAttributes { get { return Attribute0; } }

        public RMSCondition(string name, RMSNode parent, MainWindow ui) : base(name, parent, ui)
        {
            Parent = parent;
            UI = ui;
        }


        public override void Print(StreamWriter file, uint indentLevel)
        {
            string indent = new String(' ', (int)(indentLevel * 2));
            string comment = "";
            if (Comment != "")
            {
                comment = " /* " + Comment + " */ ";
            }
            file.WriteLine(indent + Name + " " + AllAttributes + comment);
            foreach (RMSNode child in Children)
            {
                child.Print(file, indentLevel + 1);
            }
        }

        public override RMSNode GetRelevantParent(RMSNode caller)
        {
            return Parent.GetRelevantParent(caller);
        }

        public override RMSNode Clone()
        {
            RMSCondition clone = new RMSCondition(Name, Parent, UI)
            {
                Attribute0 = String.Copy(Attribute0),
                Comment = String.Copy(Comment)
            };
            foreach (RMSNode child in Children)
            {
                clone.Children.Add(child.Clone());
            }
            return clone;
        }
    }
}
