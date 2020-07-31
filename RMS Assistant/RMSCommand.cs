using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMS_Assistant
{
    public class RMSCommand : RMSNode
    {
        public override List<string> AllAvailableNames
        {
            get
            {
                string relParent = GetRelevantParent(this).Name;
                return RMSNodeNameConstants.FromNameGetListCommand(relParent);
            }
        }

        public override Dictionary<string, int[]> DictNameAttributesConfigs
        {
            get
            {
                string relParent = GetRelevantParent(this).Name;
                return RMSNodeNameConstants.FromNameGetDictCommand(relParent);
            }
        }

        public override string AllAttributes
        {
            get
            {
                string allatt = "";
                for (int i = 0; (NbStringAttribute + NbIntAttribute) > i; i++)
                {
                    if      (i == 0) { allatt += Attribute0; }
                    else if (i == 1) { allatt += Attribute1; }
                    else if (i == 2) { allatt += Attribute2; }
                    else if (i == 3) { allatt += Attribute3; }
                    allatt += " ";
                }
                return allatt;
            }
        }
        public RMSCommand(string name, RMSNode parent, MainWindow ui) : base(name, parent, ui)
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
            file.WriteLine(indent + Name + " " + Attribute0 + comment);
            file.WriteLine(indent + "{");
            foreach (RMSNode child in Children)
            {
                child.Print(file, indentLevel + 1);
            }
            file.WriteLine(indent + "}");
        }

        public override RMSNode GetRelevantParent(RMSNode caller)
        {
            if (caller == this)
            {
                return Parent.GetRelevantParent(caller);
            }
            else
            {
                return this;
            }
        }

        public override RMSNode Clone()
        {
            RMSCommand clone = new RMSCommand(Name, Parent, UI)
            {

                Attribute0 = String.Copy(Attribute0),
                Comment = String.Copy(Comment),
                Min0 = Min0,
                Max0 = Max0
            };
            foreach (RMSNode child in Children)
            {
                clone.Children.Add(child.Clone());
            }
            return clone;
        }
    }
}
