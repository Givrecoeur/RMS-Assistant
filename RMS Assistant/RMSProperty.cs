using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.Windows;

namespace RMS_Assistant
{
    public class RMSProperty : RMSNode
    {
        public override List<string> AllAvailableNames
        {
            get
            {
                string relParent = GetRelevantParent(this).Name;
                return RMSNodeNameConstants.FromNameGetListProperty(relParent);
            }
        }

        public override Dictionary<string, int[]> DictNameAttributesConfigs
        {
            get
            {
                string relParent = GetRelevantParent(this).Name;
                return RMSNodeNameConstants.FromNameGetDictProperty(relParent);
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

        public RMSProperty(string name, RMSNode parent, MainWindow ui) : base(name, parent, ui)
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
            file.WriteLine(indent + Name + " " + Attribute0 + " " + Attribute1 + " " + Attribute2 + " " + Attribute3 + comment);
        }

        public override RMSNode GetRelevantParent(RMSNode caller)
        {
            return Parent.GetRelevantParent(caller);
        }

        public override RMSNode Clone()
        {
            RMSProperty clone = new RMSProperty(Name, Parent, UI)
            {
                Attribute0 = String.Copy(Attribute0),
                Min0 = Min0,
                Max0 = Max0,
                Attribute1 = String.Copy(Attribute1),
                Min1 = Min1,
                Max1 = Max1,
                Attribute2 = String.Copy(Attribute2),
                Min2 = Min2,
                Max2 = Max2,
                Attribute3 = String.Copy(Attribute3),
                Min3 = Min3,
                Max3 = Max3,
                Comment = String.Copy(Comment)
            };
            return clone;
        }
    }
}
