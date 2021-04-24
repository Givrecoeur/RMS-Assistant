using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMS_Assistant
{
    public class RMSSection : RMSNode
    {
        public override List<string> AllAvailableNames { get { return RMSNodeNameConstants.Sections.Keys.ToList(); } }

        public override Dictionary<string, int> DictNameNbAttributes { get { return RMSNodeNameConstants.Sections; } }

        public override string AllAttributes { get { return ""; } }

        public RMSSection(string name, RMSRoot root, MainWindow ui) : base(name, root, ui)
        {
            Parent = root;
            UI = ui;
        }

        public override void Print(StreamWriter file, uint indentLevel = 0)
        {
            string comment = "";
            if (Comment != "")
            {
                comment = " /* " + Comment + " */ ";
            }
            file.WriteLine("<" + Name + ">" + comment);
            foreach (RMSNode child in Children)
            {
                child.Print(file, 1);
            }
        }

        public override RMSNode GetRelevantParent(RMSNode caller)
        {
            return this;
        }

        public override RMSNode Clone()
        {
            RMSSection clone = new RMSSection(Name, Parent as RMSRoot, UI)
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
