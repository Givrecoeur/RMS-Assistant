using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace RMS_Assistant
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RMSFileManager FileManager;
        readonly ColorConstants ColorConsts;
        public RMSRoot Root;
        public RMSNode NodeInCreation;
        public XDocument XMLDocumentation;
        RMSNode CopiedNode;

        public MainWindow()
        {
            FileManager = new RMSFileManager(this);
            ColorConsts = new ColorConstants(true);
                
            InitializeComponent();
            
            Root = new RMSRoot(this);
            xamlRMSTree.ItemsSource = new BindingList<RMSRoot> { Root };
            
            Root.Children.Add(new RMSSection("PLAYER_SETUP", Root, this));
            RMSProperty test = new RMSProperty("ai_info_map_type", Root.Children[0], this);
            Root.Children[0].Children.Add(test);
            Root.Children.Add(new RMSSection("LAND_GENERATION", Root, this));
            Root.Children.Add(new RMSSection("ELEVATION_GENERATION", Root, this));
            Root.Children.Add(new RMSSection("CLIFF_GENERATION", Root, this));
            Root.Children.Add(new RMSSection("TERRAIN_GENERATION", Root, this));
            Root.Children.Add(new RMSSection("CONNECTION_GENERATION", Root, this));
            Root.Children.Add(new RMSSection("OBJECTS_GENERATION", Root, this));

            string[] allArgs = Environment.GetCommandLineArgs();
            if (allArgs.Skip(1).Any())
            {
                if (File.Exists(allArgs[1]) && System.IO.Path.GetExtension(allArgs[1]) == ".rms")
                {
                    FileManager.FileDirectory = allArgs[1];
                    FileManager.ReadFile(allArgs[1]);
                }
            }
            UpdateDarkMode(true);
            try
            {
                XMLDocumentation = XDocument.Load(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "RMSNodeDocumentation.xml"));
            }
            catch(FileNotFoundException)
            {
                System.Windows.MessageBox.Show("Documentation for nodes could not be loaded, \n the corresponding file RMSNodeDocumentation.xml is missing", "Doc error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch
            {
                System.Windows.MessageBox.Show("Documentation for nodes could not be loaded for unknown reasons, \n your file RMSNodeDocumentation.xml might be corrupted", "Doc error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

    }

        public void UpdateCreationButtons(RMSNode newNode)
        {
            if (CopiedNode != null)
            {
                if (CopiedNode is RMSConstant || CopiedNode is RMSDefine)
                {
                    PasteButton.IsEnabled = true;
                }
                else if (newNode.GetRelevantParent(CopiedNode).Name == CopiedNode.GetRelevantParent(CopiedNode).Name)
                {
                    if (CopiedNode is RMSWeigth)
                    {
                        if (newNode is RMSRandom) PasteButton.IsEnabled = true;
                        else PasteButton.IsEnabled = false;
                    }
                    else if (CopiedNode is RMSCondition)
                    {
                        if (newNode is RMSConditional) PasteButton.IsEnabled = true;
                        else PasteButton.IsEnabled = false;
                    }
                    else
                    {
                        PasteButton.IsEnabled = true;
                    }
                }
                else
                {
                    PasteButton.IsEnabled = false;
                }
            }
            else
            {
                PasteButton.IsEnabled = false;
            }

            if (newNode is RMSRoot)
            {
                NewCommandButton.IsEnabled = false;
                NewPropertyButton.IsEnabled = false;
                NewConstantButton.IsEnabled = true;
                NewDefineButton.IsEnabled = true;
                PutConditionalButton.IsEnabled = true;
                MakeConditionalButton.IsEnabled = false;
                NewConditionButton.IsEnabled = false;
                NewRandomButton.IsEnabled = true;
                RandomizeButton.IsEnabled = false;
                AddWeigthButton.IsEnabled = false;
                CopyButton.IsEnabled = false;
                NewCommentButton.IsEnabled = true;
                MoveUpButton.IsEnabled = false;
                MoveDownButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }

            if (newNode is RMSSection)
            {
                if (newNode.Name == "PLAYER_SETUP")
                {
                    NewCommandButton.IsEnabled = false;
                    NewPropertyButton.IsEnabled = true;
                }
                else if (newNode.Name == "LAND_GENERATION")
                {
                    NewCommandButton.IsEnabled = true;
                    NewPropertyButton.IsEnabled = true;
                }
                else if (newNode.Name == "ELEVATION_GENERATION")
                {
                    NewCommandButton.IsEnabled = true;
                    NewPropertyButton.IsEnabled = false;
                }
                else if (newNode.Name == "CLIFF_GENERATION")
                {
                    NewCommandButton.IsEnabled = false;
                    NewPropertyButton.IsEnabled = true;
                }
                else if (newNode.Name == "TERRAIN_GENERATION")
                {
                    NewCommandButton.IsEnabled = true;
                    NewPropertyButton.IsEnabled = true;
                }
                else if (newNode.Name == "CONNECTION_GENERATION")
                {
                    NewCommandButton.IsEnabled = true;
                    NewPropertyButton.IsEnabled = false;
                }
                else if (newNode.Name == "OBJECTS_GENERATION")
                {
                    NewCommandButton.IsEnabled = true;
                    NewPropertyButton.IsEnabled = false;
                }
                NewConstantButton.IsEnabled = true;
                NewDefineButton.IsEnabled = true;
                PutConditionalButton.IsEnabled = true;
                MakeConditionalButton.IsEnabled = false;
                NewConditionButton.IsEnabled = false;
                NewRandomButton.IsEnabled = true;
                RandomizeButton.IsEnabled = false;
                AddWeigthButton.IsEnabled = false;
                CopyButton.IsEnabled = false;
                NewCommentButton.IsEnabled = true;
                MoveUpButton.IsEnabled = true;
                MoveDownButton.IsEnabled = true;
                DeleteButton.IsEnabled = false;
            }

            else if (newNode is RMSCommand)
            {
                NewCommandButton.IsEnabled = false;
                NewPropertyButton.IsEnabled = true;
                NewConstantButton.IsEnabled = true;
                NewDefineButton.IsEnabled = true;
                PutConditionalButton.IsEnabled = true;
                MakeConditionalButton.IsEnabled = true;
                NewConditionButton.IsEnabled = false;
                NewRandomButton.IsEnabled = true;
                RandomizeButton.IsEnabled = true;
                AddWeigthButton.IsEnabled = false;
                CopyButton.IsEnabled = true;
                NewCommentButton.IsEnabled = true;
                MoveUpButton.IsEnabled = true;
                MoveDownButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }

            else if (newNode is RMSProperty)
            {
                NewCommandButton.IsEnabled = false;
                NewPropertyButton.IsEnabled = false;
                NewConstantButton.IsEnabled = false;
                NewDefineButton.IsEnabled = false;
                PutConditionalButton.IsEnabled = false;
                MakeConditionalButton.IsEnabled = true;
                NewConditionButton.IsEnabled = false;
                NewRandomButton.IsEnabled = false;
                RandomizeButton.IsEnabled = true;
                AddWeigthButton.IsEnabled = false;
                CopyButton.IsEnabled = true;
                NewCommentButton.IsEnabled = true;
                MoveUpButton.IsEnabled = true;
                MoveDownButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }

            else if (newNode is RMSDefine)
            {
                NewCommandButton.IsEnabled = false;
                NewPropertyButton.IsEnabled = false;
                NewConstantButton.IsEnabled = false;
                NewDefineButton.IsEnabled = false;
                PutConditionalButton.IsEnabled = false;
                MakeConditionalButton.IsEnabled = true;
                NewConditionButton.IsEnabled = false;
                NewRandomButton.IsEnabled = false;
                RandomizeButton.IsEnabled = true;
                AddWeigthButton.IsEnabled = false;
                CopyButton.IsEnabled = true;
                NewCommentButton.IsEnabled = true;
                MoveUpButton.IsEnabled = true;
                MoveDownButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }

            else if (newNode is RMSConstant)
            {
                NewCommandButton.IsEnabled = false;
                NewPropertyButton.IsEnabled = false;
                NewConstantButton.IsEnabled = false;
                NewDefineButton.IsEnabled = false;
                PutConditionalButton.IsEnabled = false;
                MakeConditionalButton.IsEnabled = true;
                NewConditionButton.IsEnabled = false;
                NewRandomButton.IsEnabled = false;
                RandomizeButton.IsEnabled = true;
                AddWeigthButton.IsEnabled = false;
                CopyButton.IsEnabled = true;
                NewCommentButton.IsEnabled = true;
                MoveUpButton.IsEnabled = true;
                MoveDownButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }

            else if (newNode is RMSRandom)
            {
                NewCommandButton.IsEnabled = false;
                NewPropertyButton.IsEnabled = false;
                NewConstantButton.IsEnabled = false;
                NewDefineButton.IsEnabled = false;
                PutConditionalButton.IsEnabled = false;
                MakeConditionalButton.IsEnabled = true;
                NewConditionButton.IsEnabled = false;
                NewRandomButton.IsEnabled = false;
                RandomizeButton.IsEnabled = true;
                AddWeigthButton.IsEnabled = true;
                CopyButton.IsEnabled = true;
                NewCommentButton.IsEnabled = true;
                MoveUpButton.IsEnabled = true;
                MoveDownButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }

            else if (newNode is RMSConditional)
            {
                NewCommandButton.IsEnabled = false;
                NewPropertyButton.IsEnabled = false;
                NewConstantButton.IsEnabled = false;
                NewDefineButton.IsEnabled = false;
                PutConditionalButton.IsEnabled = false;
                MakeConditionalButton.IsEnabled = true;
                NewConditionButton.IsEnabled = true;
                NewRandomButton.IsEnabled = false;
                RandomizeButton.IsEnabled = true;
                AddWeigthButton.IsEnabled = false;
                CopyButton.IsEnabled = true;
                NewCommentButton.IsEnabled = true;
                MoveUpButton.IsEnabled = true;
                MoveDownButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }

            else if (newNode is RMSComment)
            {
                NewCommandButton.IsEnabled = false;
                NewPropertyButton.IsEnabled = false;
                NewConstantButton.IsEnabled = false;
                NewDefineButton.IsEnabled = false;
                PutConditionalButton.IsEnabled = false;
                MakeConditionalButton.IsEnabled = false;
                NewConditionButton.IsEnabled = false;
                NewRandomButton.IsEnabled = false;
                RandomizeButton.IsEnabled = false;
                AddWeigthButton.IsEnabled = false;
                CopyButton.IsEnabled = false;
                NewCommentButton.IsEnabled = true;
                MoveUpButton.IsEnabled = true;
                MoveDownButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }

            else if (newNode is RMSCondition || newNode is RMSWeigth)
            {
                UpdateCreationButtons(newNode.GetRelevantParent(newNode));
                CopyButton.IsEnabled = true;
                NewCommentButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }
            if (newNode.isInRandomBlock())
            {
                RandomizeButton.IsEnabled = false;
                NewRandomButton.IsEnabled = false;
            }
        }

        public void UpdatePanels(RMSNode newNode)
        {
            if (newNode != null)
            {
                CreationNodePanel.Children.Clear();
                NodeInCreation = null;
                SelectedNodePanel.Children.Clear();
                AddNewNodeButton.IsEnabled = false;
                CancelCreationButton.IsEnabled = false;
                newNode.PrepareInterface();
                SelectedNodePanel.Children.Add(newNode.Interface);
                UpdateNodeDocPanel(false);
            }
        }

        public void UpdateNodeInCreationPanel()
        {
            if (NodeInCreation != null)
            {
                CreationNodePanel.Children.Clear();
                CreationNodePanel.Children.Add(NodeInCreation.Interface);
                UpdateNodeDocPanel(false);
            }
        }

        public void UpdateNodeDocPanel(bool isSearch)
        {
            NodeDocumentation.Children.Clear();
            NodeDocumentationScrollViewer.ScrollToVerticalOffset(0);
            RMSNode selectedNode = xamlRMSTree.SelectedItem as RMSNode;
            string nodeToSearch = "";
            string sectionFilter = "";
            if (!isSearch)
            {
                if (selectedNode != null)
                {
                    RMSNode section = selectedNode;
                    while(section.GetRelevantParent(section) != section)
                    {
                        section = section.GetRelevantParent(section);
                    }
                    sectionFilter = section.Name;
                }
                if (NodeInCreation != null)
                {
                    nodeToSearch = NodeInCreation.Name;
                }
                else
                {
                    
                    if (selectedNode != null)
                    {
                        nodeToSearch = selectedNode.Name;
                    }
                }
                if (nodeToSearch != "")
                {
                    NodeDocumentation.Children.Add(new NodeDocumentationPanel(XMLDocumentation, nodeToSearch, sectionFilter));
                }
            }
            else
            {
                nodeToSearch = NodeDocSearchField.Text;
                if (nodeToSearch != "")
                {
                    NodeDocumentation.Children.Add(new NodeDocumentationPanel(XMLDocumentation, nodeToSearch));
                }
            }
            /*if (nodeToSearch != "")
            {
                NodeDocumentation.Children.Add(new NodeDocumentationPanel(XMLDocumentation, nodeToSearch));
            }*/

        }

        public void UpdateDarkMode(bool isDarkMode)
        {
            ColorConsts.SwitchMode(isDarkMode);
            xamlRMSTree.Background = ColorConsts.TreeBackground;

            //TODO change all colors everywhere
        }


        //Controls
        private void xamlRMSTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            RMSNode newNode = (e.NewValue as RMSNode);

            if (newNode != null)
            {
                UpdateCreationButtons(newNode);
                UpdatePanels(newNode);
            }
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!FileManager.WarnForSave(Root))
            {
                e.Cancel = true;
            }
        }

        private void DisplayNewNodeInCreation()
        {
            CreationNodePanel.Children.Clear();
            NodeInCreation.PrepareInterface();
            CreationNodePanel.Children.Add(NodeInCreation.Interface);
            AddNewNodeButton.IsEnabled = true;
            CancelCreationButton.IsEnabled = true;
            UpdateNodeDocPanel(false);
        }

        private void NewCommandButton_Click(object sender, RoutedEventArgs e)
        {
            RMSNode parent = xamlRMSTree.SelectedItem as RMSNode;
            NodeInCreation = new RMSCommand(RMSNodeNameConstants.FromNameGetListCommand(parent.Name)[0], parent, this);
            DisplayNewNodeInCreation();
        }

        private void NewPropertyButton_Click(object sender, RoutedEventArgs e)
        {
            RMSNode parent = xamlRMSTree.SelectedItem as RMSNode;
            NodeInCreation = new RMSProperty(RMSNodeNameConstants.FromNameGetListProperty(parent.GetRelevantParent(null).Name)[0], parent, this); //TODO chose first unused property?
            DisplayNewNodeInCreation();
        }

        private void NewConstantButton_Click(object sender, RoutedEventArgs e)
        {
            RMSNode parent = xamlRMSTree.SelectedItem as RMSNode;
            NodeInCreation = new RMSConstant(parent, this);
            DisplayNewNodeInCreation();
        }

        private void NewDefineButton_Click(object sender, RoutedEventArgs e)
        {
            RMSNode parent = xamlRMSTree.SelectedItem as RMSNode;
            NodeInCreation = new RMSDefine(parent, this);
            DisplayNewNodeInCreation();
        }

        private void PutConditionalButton_Click(object sender, RoutedEventArgs e)
        {
            RMSNode parent = xamlRMSTree.SelectedItem as RMSNode;
            NodeInCreation = new RMSConditional(parent, this);
            DisplayNewNodeInCreation();
        }

        private void MakeConditionalButton_Click(object sender, RoutedEventArgs e)
        {
            RMSNode currentNode = xamlRMSTree.SelectedItem as RMSNode;
            currentNode.Parent.Children.Remove(currentNode);
            RMSNode newConditional = new RMSConditional(currentNode.Parent, this);
            currentNode.Parent.AddNode(newConditional);
            RMSNode condition = new RMSCondition("if", newConditional, this);
            //condition.Attribute0 = "RANDOM_MAP";
            newConditional.AddNode(condition);
            condition.AddNode(currentNode);
            currentNode.Parent = condition;
        }

        private void NewConditionButton_Click(object sender, RoutedEventArgs e)
        {
            RMSNode parent = xamlRMSTree.SelectedItem as RMSNode;
            int nbChildren = parent.Children.Count;
            if (nbChildren == 0) NodeInCreation = new RMSCondition("if", parent, this);
            else NodeInCreation = new RMSCondition("elseif", parent, this);
            DisplayNewNodeInCreation();
        }

        private void NewRandomButton_Click(object sender, RoutedEventArgs e)
        {
            RMSNode parent = xamlRMSTree.SelectedItem as RMSNode;
            NodeInCreation = new RMSRandom(parent, this);
            DisplayNewNodeInCreation();
        }

        private void RandomizeButton_Click(object sender, RoutedEventArgs e)
        {
            RMSNode currentNode = xamlRMSTree.SelectedItem as RMSNode;
            currentNode.Parent.Children.Remove(currentNode);
            RMSNode newRandom = new RMSRandom(currentNode.Parent, this);
            currentNode.Parent.AddNode(newRandom);
            RMSNode weigth = new RMSWeigth(newRandom, this);
            weigth.Attribute0 = "50";
            newRandom.AddNode(weigth);
            weigth.AddNode(currentNode);
            currentNode.Parent = weigth;
        }

        private void AddWeigthButton_Click(object sender, RoutedEventArgs e)
        {
            RMSNode parent = xamlRMSTree.SelectedItem as RMSNode;
            NodeInCreation = new RMSWeigth(parent, this);
            DisplayNewNodeInCreation();
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            CopiedNode = (xamlRMSTree.SelectedItem as RMSNode).Clone();
        }

        private void PasteButton_Click(object sender, RoutedEventArgs e)
        {
            RMSNode newParent = xamlRMSTree.SelectedItem as RMSNode;
            if (newParent.GetRelevantParent(newParent) == CopiedNode.GetRelevantParent(CopiedNode))
            {
                newParent.AddNode(CopiedNode.Clone());
                CopiedNode.Parent = newParent;
            }
        }

        private void MoveUpButton_Click(object sender, RoutedEventArgs e)
        {
            RMSNode node = xamlRMSTree.SelectedItem as RMSNode;
            if (node.Parent != null)
            {
                int index = node.Parent.Children.IndexOf(node);
                if (index > 0)
                {
                    RMSNode toMove = node.Parent.Children[index - 1];
                    node.Parent.Children.RemoveAt(index - 1);
                    if (index == node.Parent.Children.Count)
                    {
                        node.Parent.Children.Add(toMove);
                    }
                    else
                    {
                        node.Parent.Children.Insert(index, toMove);
                    }
                }
            }
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            RMSNode node = xamlRMSTree.SelectedItem as RMSNode;
            if (node.Parent != null)
            {
                int index = node.Parent.Children.IndexOf(node);
                if (node.Parent.Children.Count > index + 1)
                {
                    RMSNode toMove = node.Parent.Children[index + 1];
                    node.Parent.Children.RemoveAt(index + 1);
                    node.Parent.Children.Insert(index, toMove);
                }
            }
        }

        private void NewCommentButton_Click(object sender, RoutedEventArgs e)
        {
            RMSNode parent = xamlRMSTree.SelectedItem as RMSNode;
            NodeInCreation = new RMSComment(parent, this);
            DisplayNewNodeInCreation();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            RMSNode node = xamlRMSTree.SelectedItem as RMSNode;
            if (node.Parent != null)
            {
                int index = node.Parent.Children.IndexOf(node);
                node.Parent.Children.RemoveAt(index);
            }
        }

        private void AddNewNodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (NodeInCreation.CheckAttributesPresence())
            {
                NodeInCreation.Parent.AddNode(NodeInCreation);
                CreationNodePanel.Children.Clear();
                NodeInCreation = null;
            }
        }

        private void CancelCreationButton_Click(object sender, RoutedEventArgs e)
        {
            CreationNodePanel.Children.Clear();
            NodeInCreation = null;
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            FileManager.OpenFile(Root);
            RMSNode node = xamlRMSTree.SelectedItem as RMSNode;
            UpdatePanels(node);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FileManager.SaveFile(Root);
        }

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            FileManager.SaveFile(Root, true);
        }

        private void IsDarkMode_Checked(object sender, RoutedEventArgs e)
        {

            UpdateDarkMode(true);
        }

        private void IsDarkMode_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateDarkMode(false);
        }

        public void UpdateProgress(int progress, string message)
        {
            if (progress < 0)
            {
                ProgressBar.Visibility = Visibility.Hidden;
                ProgressStatus.Visibility = Visibility.Hidden;
            }
            else
            {
                ProgressBar.Visibility = Visibility.Visible;
                ProgressStatus.Visibility = Visibility.Visible;
                ProgressBar.Value = progress;
                ProgressStatus.Content = message;
            }
        }

        private void Button_DocSearch_Click(object sender, RoutedEventArgs e)
        {
            if (NodeDocSearchField.Text != "")
            {
                UpdateNodeDocPanel(true);
            }
        }

        private void Button_DocCancel_Click(object sender, RoutedEventArgs e)
        {
            UpdateNodeDocPanel(false);
        }

        private void DiscordButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.com/invite/KbTKFpJ");
        }

        private void AboutThisButton_Click(object sender, RoutedEventArgs e)
        {
            return; //TODO
        }

        private void DocumentationButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.google.com/document/d/1jnhZXoeL9mkRUJxcGlKnO98fIwFKStP_OBozpr0CHXo/edit#heading=h.ehe5dkiu96so");
        }

        
    }


}
