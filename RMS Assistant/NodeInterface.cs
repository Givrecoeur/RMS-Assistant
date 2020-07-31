using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RMS_Assistant
{
    public class NodeInterface : StackPanel
    {
        public RMSNode Node;

        public DockPanel NamePannel;
        public StackPanel AttributesPannel;
        public StackPanel CommentPannel;

        public ComboBox Names;
        public TextBox Comment;


        public Button MoveDown;
        public Button MoveUp;
        public Button DeleteNode;
        public Button Select;

        public NodeInterface(RMSNode node)
        {
            Node = node;
            Orientation = Orientation.Vertical;

            //Declarations
            NamePannel = new DockPanel();
            AttributesPannel = new StackPanel();
            AttributesPannel.Orientation = Orientation.Horizontal;
            AttributesPannel.Height = 40;
            AttributesPannel.Width = 400;
            CommentPannel = new StackPanel();
            CommentPannel.Orientation = Orientation.Horizontal;
            CommentPannel.Height = 40;
            CommentPannel.Width = 400;

            Names = new ComboBox();
            Names.Width = 280;
            Names.Height = 30;
            Names.Margin = new Thickness(5);
            Names.SelectionChanged += Names_SelectionChanged;

            NamePannel.Children.Add(Names);
            DockPanel.SetDock(Names, Dock.Left);

            Label commentLabel = new Label();
            commentLabel.Content = "Note : ";
            commentLabel.Margin = new Thickness(0, 5, 0, 0);
            Comment = new TextBox();
            Comment.Width = 350;
            Comment.Height = 20;
            Comment.TextChanged += Comment_TextChanged;
            CommentPannel.Children.Add(commentLabel);
            CommentPannel.Children.Add(Comment);

            Children.Add(NamePannel);
            Children.Add(AttributesPannel);
            Children.Add(CommentPannel);

        }

        private void Comment_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox comment = (TextBox)sender;
            Node.Comment = comment.Text;
        }

        private void Names_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool nameHasChanged = Node.Name != e.AddedItems[0].ToString();
            Node.Name = e.AddedItems[0].ToString();
            if (Node.Parent != null && !Node.Parent.Children.Contains(Node) && nameHasChanged) //This node is not yet added to its parent Children list, therefore it is still in creation
            {
                Node.PrepareInterface();
                Node.UI.UpdateNodeInCreationPanel();
            }
        }
    }


    public abstract class AttributeDisplayer : Grid
    {
        public Label TypeOfAttribute;
        public TextBox AttributeValue;
    }

    public class StringAttributeDisplayer : AttributeDisplayer
    {
        

        public StringAttributeDisplayer()
        {
            TypeOfAttribute = new Label();
            TypeOfAttribute.Margin = new Thickness(0, 3, 0, 0);
            AttributeValue = new TextBox();
            AttributeValue.Height = 20;

            Height = 30;
            ColumnDefinition col0 = new ColumnDefinition();
            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinitions.Add(col0);
            ColumnDefinitions.Add(col1);

            Width = 150;
            col0.Width = new GridLength(40);
            col1.Width = new GridLength(110);
            TypeOfAttribute.Content = "TYPE";
            TypeOfAttribute.ToolTip = "A constant identifier such as GRASS";

            Children.Add(TypeOfAttribute);
            Children.Add(AttributeValue);
            SetColumn(TypeOfAttribute, 0);
            SetColumn(AttributeValue, 1);
        }
    }

    public class IntAttributeDisplayer : AttributeDisplayer
    {
        int MinValue;
        int MaxValue;

        public IntAttributeDisplayer(int minValue, int maxValue)
        {
            TypeOfAttribute = new Label();
            TypeOfAttribute.Margin = new Thickness(0, 3, 0, 0);
            AttributeValue = new TextBox();
            AttributeValue.Height = 20;

            MinValue = minValue;
            MaxValue = maxValue;

            Height = 30;
            ColumnDefinition col0 = new ColumnDefinition();
            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinitions.Add(col0);
            ColumnDefinitions.Add(col1);

            Width = 80;
            col0.Width = new GridLength(30);
            col1.Width = new GridLength(50);
            if (MinValue == 0 && MaxValue == 100)
            {
                TypeOfAttribute.Content = "%";
                TypeOfAttribute.ToolTip = "A percentage";
            }
            else
            {
                TypeOfAttribute.Content = "N";
                TypeOfAttribute.ToolTip = "An integer";
                if (MinValue < MaxValue)
                {
                    TypeOfAttribute.ToolTip += " (between " + MinValue.ToString() + " and " + MaxValue.ToString() + ")";
                }

            }
            /*AttributeValue.PreviewTextInput += AttributeValue_PreviewTextInput;
            AttributeValue.TextChanged += AttributeValue_TextChanged;*/

            Children.Add(TypeOfAttribute);
            Children.Add(AttributeValue);
            SetColumn(TypeOfAttribute, 0);
            SetColumn(AttributeValue, 1);
        }

        /*private void AttributeValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox.Text != "")
            {
                bool isInt = int.TryParse(textbox.Text, out int value);
                if (isInt)
                {
                    if (MinValue < MaxValue && value < MinValue) textbox.Text = MinValue.ToString();
                    else if (MinValue < MaxValue && value > MaxValue) textbox.Text = MaxValue.ToString();
                }
                else
                {
                    throw new Exception("Invalid value for an int");
                }
            }
        }

        private void AttributeValue_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            bool isInt = int.TryParse(e.Text, out int value);
            if (isInt)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }*/
    }
}
