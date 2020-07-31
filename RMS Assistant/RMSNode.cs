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
                if (DictNameAttributesConfigs.ContainsKey(value))
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
        protected int NbStringAttribute;
        protected int NbIntAttribute;
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
        protected int Min0;
        protected int Max0;
        protected int Min1;
        protected int Max1;
        protected int Min2;
        protected int Max2;
        protected int Min3;
        protected int Max3;
        public abstract Dictionary<string, int[]> DictNameAttributesConfigs { get; }
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
                ChangeAttributeConfig(DictNameAttributesConfigs[Name]);
            }
        }

        private void Children_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.PropertyDescriptor != null && e.PropertyDescriptor.Name == "Name")
            {
                UI.UpdatePanels(Children[e.NewIndex]);
            }
        }

        public void ChangeAttributeConfig(int[] newConfig)
        {
            if (newConfig.Length >= 10)
            {
                NbStringAttribute = newConfig[0];
                NbIntAttribute = newConfig[1];

                Min0 = newConfig[2];
                Max0 = newConfig[3];
                Min1 = newConfig[4];
                Max1 = newConfig[5];
                Min2 = newConfig[6];
                Max2 = newConfig[7];
                Min3 = newConfig[8];
                Max3 = newConfig[9];
            }
            else
            {
                throw new Exception("Erroneous attribute config");
            }
        }

        public void PrepareInterface()
        {
        if (DictNameAttributesConfigs.ContainsKey(Name))
            {
               ChangeAttributeConfig(DictNameAttributesConfigs[Name]);
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

            int currentAttributeNb = 0;
            for (int i = 0; i < NbStringAttribute; i++)
            {
                StringAttributeDisplayer attributeInterface = new StringAttributeDisplayer(); //USE COMBOBOX WITH IsEditable to True with Items linked to all available type (with scrollbar)
                Binding myBinding;// = new Binding();

                if (currentAttributeNb == 0)
                {
                    myBinding = new Binding("Attribute0");
                    attributeInterface.AttributeValue.TextChanged += Attribute0Value_TextChanged;
                    attributeInterface.AttributeValue.Text = Attribute0;

                }
                else // currentAttributeNb == 1
                {
                    myBinding = new Binding("Attribute1");
                    attributeInterface.AttributeValue.TextChanged += Attribute1Value_TextChanged;
                    attributeInterface.AttributeValue.Text = Attribute1;
                }
                myBinding.Source = this;
                attributeInterface.AttributeValue.SetBinding(TextBlock.TextProperty, myBinding); //selectedItem
                Interface.AttributesPannel.Children.Add(attributeInterface);

                currentAttributeNb++;
            }

            for (int i = 0; i < NbIntAttribute; i++)
            {
                IntAttributeDisplayer attributeInterface;
                Binding myBinding;
                if (currentAttributeNb == 0)
                {
                    attributeInterface = new IntAttributeDisplayer(Min0, Max0);
                    myBinding = new Binding("Attribute0");
                    attributeInterface.AttributeValue.TextChanged += Attribute0Value_TextChanged;
                    attributeInterface.AttributeValue.Text = Attribute0;

                }
                else if (currentAttributeNb == 1)
                {
                    attributeInterface = new IntAttributeDisplayer(Min1, Max1);
                    myBinding = new Binding("Attribute1");
                    attributeInterface.AttributeValue.TextChanged += Attribute1Value_TextChanged;
                    attributeInterface.AttributeValue.Text = Attribute1;
                }
                else if (currentAttributeNb == 2)
                {
                    attributeInterface = new IntAttributeDisplayer(Min2, Max2);
                    myBinding = new Binding("Attribute2");
                    attributeInterface.AttributeValue.TextChanged += Attribute2Value_TextChanged;
                    attributeInterface.AttributeValue.Text = Attribute2;
                }
                else // currentAttributeNb == 3
                {
                    attributeInterface = new IntAttributeDisplayer(Min3, Max3);
                    myBinding = new Binding("Attribute3");
                    attributeInterface.AttributeValue.TextChanged += Attribute3Value_TextChanged;
                    attributeInterface.AttributeValue.Text = Attribute3;
                }
                myBinding.Source = this;
                attributeInterface.AttributeValue.SetBinding(TextBlock.TextProperty, myBinding);
                Interface.AttributesPannel.Children.Add(attributeInterface);

                currentAttributeNb++;
            }
            Interface.Comment.Text = Comment;
            Interface.UpdateLayout();
        }

        public bool CheckAttributesPresence()
        {
            bool AllOK = true;
            for (int i = 0; i < NbStringAttribute + NbIntAttribute; i++)
            {
                if (i == 0)
                {
                    AttributeDisplayer display = Interface.AttributesPannel.Children[0] as AttributeDisplayer;
                    if (Attribute0 == "") { display.AttributeValue.BorderBrush = new SolidColorBrush(Colors.Red); AllOK = false; }
                    else display.AttributeValue.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                else if (i == 1)
                {
                    AttributeDisplayer display = Interface.AttributesPannel.Children[1] as AttributeDisplayer;
                    if (Attribute1 == "") { display.AttributeValue.BorderBrush = new SolidColorBrush(Colors.Red); AllOK = false; }
                    else display.AttributeValue.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                else if (i == 2)
                {
                    AttributeDisplayer display = Interface.AttributesPannel.Children[2] as AttributeDisplayer;
                    if (Attribute2 == "") { display.AttributeValue.BorderBrush = new SolidColorBrush(Colors.Red); AllOK = false; }
                    else display.AttributeValue.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                else // (i == 3)
                {
                    AttributeDisplayer display = Interface.AttributesPannel.Children[3] as AttributeDisplayer;
                    if (Attribute3 == "") { display.AttributeValue.BorderBrush = new SolidColorBrush(Colors.Red); AllOK = false; }
                    else display.AttributeValue.BorderBrush = new SolidColorBrush(Colors.Black);
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

    }

}
