using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace RMS_Assistant
{

    abstract public class RMSNode : INotifyPropertyChanged
    {
        public RMSNode Parent { get; set; }
        public NodeInterface Interface;
        public MainWindow UI;
        public BindingList<RMSNode> Children { get; set; }
        public string Color { get; set; }
        public string AttributeColor { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        //Name
        protected string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (value != _Name)
                {
                    Attribute0 = "";
                    Attribute1 = "";
                    Attribute2 = "";
                    Attribute3 = "";
                }
                if (DictNameNbAttributes.ContainsKey(value))
                {
                    SetField(ref _Name, value, "Name");
                }
                else
                {
                    throw new Exception("Unknown node, can't be created");
                }
            }
        }

        protected string _Comment;
        public string Comment
        {
            get
            {
                return _Comment;
            }
            set
            {
                SetField(ref _Comment, value, "Comment");
            }
        }

        public abstract List<string> AllAvailableNames { get; }

        //Attributes
        protected int NbAttribute;
        private string _Attribute0 = "";
        public string Attribute0
        {
            get
            {
                return _Attribute0;
            }
            set
            {
                SetField(ref _Attribute0, value, "Attribute0");
            }
        }
        private string _Attribute1 = "";
        public string Attribute1
        {
            get
            {
                return _Attribute1;
            }
            set
            {
                SetField(ref _Attribute1, value, "Attribute1");
            }
        }
        private string _Attribute2 = "";
        public string Attribute2
        {
            get
            {
                return _Attribute2;
            }
            set
            {
                SetField(ref _Attribute2, value, "Attribute2");
            }
        }
        private string _Attribute3 = "";
        public string Attribute3
        {
            get
            {
                return _Attribute3;
            }
            set
            {
                SetField(ref _Attribute3, value, "Attribute3");
            }
        }
        public abstract Dictionary<string, int> DictNameNbAttributes { get; }
        abstract public string AllAttributes { get; }


        public RMSNode(string name, RMSNode parent, MainWindow ui)
        {
            Parent = parent;
            Name = name;
            UI = ui;

            Comment = "";
            Color = "Black";
            AttributeColor = "Blue";
            Parent = null;

            Children = new BindingList<RMSNode>();
            Children.ListChanged += Children_ListChanged;

            if (Parent != null)
            {
                NbAttribute = DictNameNbAttributes[Name];
            }
        }

        private void Children_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.PropertyDescriptor != null && e.PropertyDescriptor.Name == "Name")
            {
                UI.UpdatePanels(Children[e.NewIndex]);
            }
        }

        public void PrepareInterface()
        {
        if (DictNameNbAttributes.ContainsKey(Name))
            {
               NbAttribute = DictNameNbAttributes[Name];
               SetUpInterface();
            }
        }

        public void SetUpInterface()
        {
            Interface = new NodeInterface(this);

            foreach (string name in AllAvailableNames)
            {
                Interface.Names.Items.Add(name);
            }
            int index = Interface.Names.Items.IndexOf(Name);
            int selectedIndex = Interface.Names.SelectedIndex;
            Interface.Names.SelectedIndex = index;

            if (NbAttribute > 0)
            {
                Label ArgsLabel = new Label
                {
                    Content = "Args :",
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 5, 0, 0)
                };
                Interface.AttributesPannel.Children.Add(ArgsLabel);
            }

            int currentAttributeNb = 0;
            for (int i = 0; i < NbAttribute; i++)
            {
                AttributeDisplay attributeInterface = new AttributeDisplay(); //USE COMBOBOX WITH IsEditable to True with Items linked to all available type (with scrollbar)
                Binding myBinding;// = new Binding();

                if (currentAttributeNb == 0)
                {
                    myBinding = new Binding("Attribute0");
                    attributeInterface.TextChanged += Attribute0Value_TextChanged;
                    attributeInterface.Text = Attribute0;

                }
                else if (currentAttributeNb == 1)
                {
                    myBinding = new Binding("Attribute1");
                    attributeInterface.TextChanged += Attribute1Value_TextChanged;
                    attributeInterface.Text = Attribute1;
                }
                else if (currentAttributeNb == 2)
                {
                    myBinding = new Binding("Attribute2");
                    attributeInterface.TextChanged += Attribute2Value_TextChanged;
                    attributeInterface.Text = Attribute2;
                }
                else // currentAttributeNb == 3
                {
                    myBinding = new Binding("Attribute3");
                    attributeInterface.TextChanged += Attribute3Value_TextChanged;
                    attributeInterface.Text = Attribute3;
                }
                myBinding.Source = this;
                attributeInterface.SetBinding(TextBlock.TextProperty, myBinding); //selectedItem
                Interface.AttributesPannel.Children.Add(attributeInterface);

                currentAttributeNb++;
            }
            Interface.Comment.Text = Comment;
            Interface.UpdateLayout();
        }

        public bool CheckAttributesPresence()
        {
            bool AllOK = true;
            for (int i = 1; i < NbAttribute + 1; i++) //i = 0 is a label object
            {
                if (i == 1)
                {
                    AttributeDisplay display = Interface.AttributesPannel.Children[1] as AttributeDisplay;
                    if (Attribute0 == "") { display.BorderBrush = new SolidColorBrush(Colors.Red); AllOK = false; }
                    else display.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                else if (i == 2)
                {
                    AttributeDisplay display = Interface.AttributesPannel.Children[2] as AttributeDisplay;
                    if (Attribute1 == "") { display.BorderBrush = new SolidColorBrush(Colors.Red); AllOK = false; }
                    else display.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                else if (i == 3)
                {
                    AttributeDisplay display = Interface.AttributesPannel.Children[3] as AttributeDisplay;
                    if (Attribute2 == "") { display.BorderBrush = new SolidColorBrush(Colors.Red); AllOK = false; }
                    else display.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                else // (i == 3)
                {
                    AttributeDisplay display = Interface.AttributesPannel.Children[4] as AttributeDisplay;
                    if (Attribute3 == "") { display.BorderBrush = new SolidColorBrush(Colors.Red); AllOK = false; }
                    else display.BorderBrush = new SolidColorBrush(Colors.Black);
                }
            }
            return AllOK;
        }

        private void Attribute0Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox attribute = (TextBox)sender;
            Attribute0 = attribute.Text;
        }

        private void Attribute1Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox attribute = (TextBox)sender;
            Attribute1 = attribute.Text;
        }

        private void Attribute2Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox attribute = (TextBox)sender;
            Attribute2 = attribute.Text;
        }

        private void Attribute3Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox attribute = (TextBox)sender;
            Attribute3 = attribute.Text;
        }

        public virtual void AddNode(RMSNode newChild)
        {
            Children.Add(newChild);
        }

        public abstract void Print(StreamWriter file, uint indentLevel);
        public abstract RMSNode GetRelevantParent(RMSNode caller);
        public abstract RMSNode Clone();

        public bool isInRandomBlock()
        {
            bool isRandomized = false;
            RMSNode node = this;
            while (node != null)
            {
                if (node is RMSRandom)
                {
                    isRandomized = true;
                    break;
                }
                node = node.Parent;
            }
            return isRandomized;
        }

    }

}
