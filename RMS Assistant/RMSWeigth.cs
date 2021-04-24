using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMS_Assistant
{
    class RMSWeigth : RMSNode
    {
        public override List<string> AllAvailableNames { get { return RMSNodeNameConstants.Weigths.Keys.ToList(); } }

        public override Dictionary<string, int> DictNameNbAttributes { get { return RMSNodeNameConstants.Weigths; } }

        public override string AllAttributes { get { return Attribute0; } }

        public RMSWeigth(RMSNode parent, MainWindow ui) : base("percent_chance", parent, ui)
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
            file.WriteLine(indent + "percent_chance" + " " + AllAttributes + comment);
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
            RMSWeigth clone = new RMSWeigth(Parent, UI)
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
