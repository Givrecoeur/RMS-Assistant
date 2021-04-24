using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMS_Assistant
{
    class RMSRandom : RMSNode
    {
        public override List<string> AllAvailableNames { get { return RMSNodeNameConstants.Randoms.Keys.ToList(); } }

        public override Dictionary<string, int> DictNameNbAttributes { get { return RMSNodeNameConstants.Randoms; } }

        public override string AllAttributes { get { return ""; } }

        public RMSRandom(RMSNode parent, MainWindow ui) : base("start_random", parent, ui)
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
            file.WriteLine(indent + "start_random" + comment);
            foreach (RMSNode child in Children)
            {
                child.Print(file, indentLevel + 1);
            }
            file.WriteLine(indent + "end_random");
    
        }

        public override RMSNode GetRelevantParent(RMSNode caller)
        {
            return Parent.GetRelevantParent(caller);
        }

        public override RMSNode Clone()
        {
            RMSRandom clone = new RMSRandom(Parent, UI)
            {
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
