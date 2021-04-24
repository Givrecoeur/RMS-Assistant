using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace RMS_Assistant
{
    class RMSConditional : RMSNode
    {
        public override List<string> AllAvailableNames { get { return RMSNodeNameConstants.Conditionals.Keys.ToList(); } }

        public override Dictionary<string, int> DictNameNbAttributes { get { return RMSNodeNameConstants.Conditionals; } }

        public override string AllAttributes { get { return ""; } }

        public RMSConditional(RMSNode parent, MainWindow ui) : base("Condition", parent, ui)
        {
            Parent = parent;
            UI = ui;
        }

        public override void Print(StreamWriter file, uint indentLevel)
        {
            string indent = new String(' ', (int)(indentLevel * 2));
            foreach (RMSNode child in Children)
            {
                child.Print(file, indentLevel); //no indent incrementation is itentionnal
            }
            file.WriteLine(indent + "endif");
        }

        public override RMSNode GetRelevantParent(RMSNode caller)
        {
            return Parent.GetRelevantParent(caller);
        }

        public override RMSNode Clone()
        {
            RMSConditional clone = new RMSConditional(Parent, UI)
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
