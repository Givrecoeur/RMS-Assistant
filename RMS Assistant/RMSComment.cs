using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace RMS_Assistant
{
    class RMSComment : RMSNode
    {
        private readonly List<string> Names = new List<string> { "Comment" };
        public override List<string> AllAvailableNames { get { return Names; } }

        private readonly Dictionary<string, int> Config = new Dictionary<string, int>
        {
            {"Comment", 0 }
        };
        public override Dictionary<string, int> DictNameNbAttributes { get { return Config; } }
        public override string AllAttributes { get { return Comment; } }

        public RMSComment(RMSNode parent, MainWindow ui) : base("Comment", parent, ui)
        {
            Parent = parent;
            UI = ui;
        }

        public override RMSNode Clone()
        {
            RMSConstant clone = new RMSConstant(Parent, UI)
            {
                Comment = String.Copy(Comment)
            };
            return clone;
        }

        public override RMSNode GetRelevantParent(RMSNode caller)
        {
            return Parent.GetRelevantParent(caller);
        }

        public override void Print(StreamWriter file, uint indentLevel)
        {
            file.WriteLine("/* " + Comment + " */ ");
        }
    }
}
