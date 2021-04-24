using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_Assistant
{
    public class RMSRoot : RMSNode
    {
        public bool NeedSave;
        public bool useCliff = true;
        private readonly List<string> Names = new List<string> { "ROOT" };
        public override List<string> AllAvailableNames { get { return Names; } }

        private readonly Dictionary<string, int> Config = new Dictionary<string, int>
        {
            {"ROOT", 0 }
        };
        public override Dictionary<string, int> DictNameNbAttributes { get { return Config; } }

        public override string AllAttributes { get { return ""; } }

        public RMSRoot(MainWindow ui) : base("ROOT", null, ui)
        {
            Name = "ROOT";
            Parent = null;
            UI = ui;
            NeedSave = false;
        }

        public override RMSNode GetRelevantParent(RMSNode caller)
        {
            return this;
        }

        public override void Print(StreamWriter file, uint indentLevel)
        {
            throw new Exception("This should not be called");
        }

        public override void AddNode(RMSNode newChild)
        {
            if (Children.Count == 0)
            {
                Children.Add(newChild);
                return;
            }
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i] is RMSSection)
                {
                    Children.Insert(i, newChild);
                    return;
                }
            }
            Children.Add(newChild);
        }

        public override RMSNode Clone()
        {
            RMSRoot clone = new RMSRoot(UI);
            foreach (RMSNode child in Children)
            {
                clone.Children.Add(child.Clone());
            }
            return clone;
        }

        public void ManageCliffSection()
        {
            if (useCliff)
            {
                bool haveCliffSection = false;
                foreach (RMSNode child in Children)
                {
                    if (child.Name == "CLIFF_GENERATION")
                    {
                        haveCliffSection = true;
                        break;
                    }
                }
                if (!haveCliffSection)
                {
                    Children.Add(new RMSSection("CLIFF_GENERATION", this, UI));
                }
            }
            else
            {
                foreach (RMSNode child in Children.ToList())
                {
                    if (child.Name == "CLIFF_GENERATION")
                    {
                        Children.Remove(child);
                    }
                }
            }
        }


    }
}
