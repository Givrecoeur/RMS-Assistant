using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace RMS_Assistant
{

    public class NodeDocumentationPanel : StackPanel
    {
        public NodeDocumentationPanel(XDocument XMLTree, string nodeName, string sectionName = "")
        {
            Orientation = Orientation.Vertical;
            TextBlock nameTextBlock = new TextBlock
            {
                Text = nodeName,
                Foreground = Brushes.DarkBlue,
                FontSize = 18,
                FontWeight = FontWeights.Bold,

                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5, 0, 0, 0)
            };
            

            try
            {
                IEnumerable<XElement> docEntries =
                    from entry in XMLTree.Descendants("Node")
                    where ((string)entry.Attribute("Name").Value == nodeName && (entry.Attribute("Location").Value.Contains(sectionName) 
                                                                              || entry.Attribute("Location").Value == "Any"
                                                                              || entry.Attribute("Location").Value == "Random Block"
                                                                              || entry.Attribute("Location").Value == "Conditional Block"))
                    select entry;

                if (docEntries.Any())
                {
                    foreach (XElement XMLNode in docEntries)
                    {
                        //Load infos from documentation
                        string nodeLocation = "";
                        if (XMLNode.Attribute("Location") != null)
                        {
                            nodeLocation = XMLNode.Attribute("Location").Value;
                        }

                        string nodeType = "";
                        if (XMLNode.Attribute("Type") != null)
                        {
                            nodeType = XMLNode.Attribute("Type").Value;
                        }

                        string gameVersions = "";
                        if (XMLNode.Attribute("GameVersion") != null)
                        {
                            gameVersions = XMLNode.Attribute("GameVersion").Value;
                        }

                        List<string> incompatibilities = new List<string>();
                        int numIncomp = 1;
                        while (XMLNode.Element("Incompatibility" + numIncomp.ToString()) != null)
                        {
                            incompatibilities.Add(XMLNode.Element("Incompatibility" + numIncomp.ToString()).Value);
                            numIncomp += 1;
                        }

                        List<string> externalRefs = new List<string>();
                        List<string> externalLinks = new List<string>();
                        int numRefs = 1;
                        while (XMLNode.Element("ExternalReference" + numRefs.ToString()) != null)
                        {
                            IEnumerable<XElement> XMLExts =
                                from entry in XMLNode.Elements("ExternalReference" + numRefs.ToString())
                                select entry;

                            if (XMLExts.Any())
                            {
                                XElement XMLLink = XMLExts.First();
                                externalRefs.Add(XMLNode.Element("ExternalReference" + numRefs.ToString()).Value);
                                externalLinks.Add(XMLLink.Attribute("Link").Value);
                            }
                            numRefs += 1;
                        }

                        List<string> arguments = new List<string>();
                        List<string> argumentsNames = new List<string>();
                        int numArgument = 1;
                        while (XMLNode.Element("Argument" + numArgument.ToString()) != null)
                        {
                            IEnumerable<XElement> XMLArgs =
                                from entry in XMLNode.Elements("Argument" + numArgument.ToString())
                                select entry;

                            if (XMLArgs.Any())
                            {
                                XElement XMLArgName = XMLArgs.First();
                                arguments.Add(XMLNode.Element("Argument" + numArgument.ToString()).Value);
                                argumentsNames.Add(XMLArgName.Attribute("Name").Value);
                            }
                            numArgument += 1;
                        }

                        string description = "";
                        if (XMLNode.Element("Description") != null)
                        {
                            description = XMLNode.Element("Description").Value;
                        }

                        List<string> additionalInfo = new List<string>();
                        int numInfo = 1;
                        while (XMLNode.Element("AdditionalInfo" + numInfo.ToString()) != null)
                        {
                            additionalInfo.Add(XMLNode.Element("AdditionalInfo" + numInfo.ToString()).Value);
                            numInfo += 1;
                        }

                        List<string> bugs = new List<string>();
                        int numBugs = 1;
                        while (XMLNode.Element("Bug" + numBugs.ToString()) != null)
                        {
                            bugs.Add(XMLNode.Element("Bug" + numBugs.ToString()).Value);
                            numBugs += 1;
                        }

                        List<string> examples = new List<string>();
                        List<string> examplesDesc = new List<string>();
                        int numExample = 1;
                        while (XMLNode.Element("Example" + numExample.ToString()) != null)
                        {
                            IEnumerable<XElement> XMLExemple =
                                from entry in XMLNode.Elements("Example" + numExample.ToString())
                                select entry;

                            if (XMLExemple.Any())
                            {
                                XElement XMLExempleDesc = XMLExemple.First();
                                examples.Add(XMLNode.Element("Example" + numExample.ToString()).Value);
                                examplesDesc.Add(XMLExempleDesc.Attribute("Description").Value);
                            }
                            numExample += 1;
                        }

                        //Write everything inside the panel
                        //Location and Type
                        TextBlock typeTextBlock = new TextBlock
                        {
                            Text = nodeLocation + "\\" + nodeType,
                            Foreground = Brushes.DarkBlue,
                            FontSize = 10,
                            FontStyle = FontStyles.Italic,

                            TextWrapping = TextWrapping.Wrap,
                            Margin = new Thickness(15, 0, 0, 0)
                        };
                        Children.Add(typeTextBlock);

                        //Name
                        nameTextBlock = new TextBlock
                        {
                            Text = nodeName,
                            Foreground = Brushes.DarkBlue,
                            FontSize = 18,
                            FontWeight = FontWeights.Bold,

                            TextWrapping = TextWrapping.Wrap,
                            Margin = new Thickness(5, 0, 0, 0)
                        };
                        Children.Add(nameTextBlock);


                        //Node signature
                        if (argumentsNames.Any())
                        {
                            string signature = "";
                            foreach (string name in argumentsNames)
                            {
                                signature += name + "   ";
                            }
                            TextBlock signatureTextBlock = new TextBlock
                            {
                                Text = signature,
                                Foreground = Brushes.BlueViolet,
                                FontSize = 12,
                                FontWeight = FontWeights.Bold,

                                TextWrapping = TextWrapping.Wrap,
                                Margin = new Thickness(10, 0, 0, 0)
                            };
                            Children.Add(signatureTextBlock);
                        }



                        //Compatible game version(s)
                        TextBlock gameVersionsTextBlock = new TextBlock
                        {
                            Text = "Compatible game versions : " + gameVersions,
                            Foreground = Brushes.Black,
                            FontSize = 12,
                            FontWeight = FontWeights.Bold,

                            TextWrapping = TextWrapping.Wrap,
                            Margin = new Thickness(5, 3, 0, 0),

                        };
                        Children.Add(gameVersionsTextBlock);

                        //Incompatibilities
                        if (incompatibilities.Any())
                        {
                            string allIncompatibilities = "Mutually exclusive with : ";
                            foreach (string incompatibility in incompatibilities)
                            {
                                allIncompatibilities += incompatibility + " ";
                            }
                            TextBlock incompatibiltiesTextBlock = new TextBlock
                            {
                                Text = allIncompatibilities,
                                Foreground = Brushes.DarkRed,
                                FontSize = 12,
                                FontWeight = FontWeights.Bold,

                                TextWrapping = TextWrapping.Wrap,
                                Margin = new Thickness(5, 3, 0, 0)
                            };
                            Children.Add(incompatibiltiesTextBlock);
                        }

                        //External references
                        if (externalRefs.Any())
                        {
                            TextBlock extRefTextBlock = new TextBlock
                            {
                                Text = "External references : ",
                                Foreground = Brushes.Black,
                                FontSize = 12,
                                FontWeight = FontWeights.Bold,

                                TextWrapping = TextWrapping.Wrap,
                                Margin = new Thickness(5, 3, 0, 0)
                            };
                            Children.Add(extRefTextBlock);
                            for (int i = 0; i < externalRefs.Count(); i++)
                            {
                                LinkButton linkButton = new LinkButton(externalLinks[i], externalRefs[i]);
                                Children.Add(linkButton);
                            }
                        }

                        //Arguments
                        TextBlock argumentsTextBlock = new TextBlock
                        {
                            Text = "Arguments :",
                            FontSize = 12,
                            FontWeight = FontWeights.Bold,

                            TextWrapping = TextWrapping.Wrap,
                        };
                        Children.Add(argumentsTextBlock);
                        if (arguments.Any())
                        {
                            for (int i = 0; i < arguments.Count(); i++)
                            {
                                ColumnDefinition col0 = new ColumnDefinition();
                                ColumnDefinition col1 = new ColumnDefinition();
                                Grid ArgGrid = new Grid
                                {
                                    Margin = new Thickness(20, 0, 0, 0),
                                };
                                ArgGrid.ColumnDefinitions.Add(col0);
                                ArgGrid.ColumnDefinitions.Add(col1);
                                col0.Width = new GridLength(1, GridUnitType.Star);
                                col1.Width = new GridLength(4, GridUnitType.Star);

                                TextBlock argNameTextBlock = new TextBlock
                                {
                                    Text = argumentsNames[i] + " :",
                                    Foreground = Brushes.BlueViolet,
                                    FontSize = 12,
                                    FontWeight = FontWeights.Bold,

                                    TextWrapping = TextWrapping.Wrap
                                };
                                ArgGrid.Children.Add(argNameTextBlock);
                                Grid.SetColumn(argNameTextBlock, 0);

                                TextBlock argumentsDescTextBlock = new TextBlock
                                {
                                    Text = arguments[i],
                                    FontSize = 12,
                                    Foreground = Brushes.Black,

                                    TextWrapping = TextWrapping.Wrap
                                };
                                ArgGrid.Children.Add(argumentsDescTextBlock);
                                Grid.SetColumn(argumentsDescTextBlock, 1);

                                Children.Add(ArgGrid);
                            }
                        }
                        else
                        {
                            TextBlock noArgumentsTextBlock = new TextBlock
                            {
                                Text = "This node do not take any argument",

                                TextWrapping = TextWrapping.Wrap
                            };
                            Children.Add(noArgumentsTextBlock);
                        }

                        //Description
                        TextBlock descTextBlock = new TextBlock
                        {
                            Text = description,
                            FontSize = 12,
                            Foreground = Brushes.Black,

                            TextWrapping = TextWrapping.Wrap,
                            Margin = new Thickness(0, 3, 0, 0)
                        };
                        Children.Add(descTextBlock);

                        //Additional info
                        if (additionalInfo.Any())
                        {
                            foreach (string addInfo in additionalInfo)
                            {
                                TextBlock addInfoTextBlock = new TextBlock
                                {
                                    Text = "- " + addInfo,
                                    FontSize = 12,
                                    Foreground = Brushes.Black,

                                    TextWrapping = TextWrapping.Wrap,
                                    Margin = new Thickness(20, 0, 0, 0)
                                };
                                Children.Add(addInfoTextBlock);
                            }
                        }

                        //Known bugs
                        if (bugs.Any())
                        {
                            TextBlock bugsTextBlock = new TextBlock
                            {
                                Text = "Known bugs :",
                                FontSize = 12,
                                Foreground = Brushes.Black,
                                FontWeight = FontWeights.Bold,

                                TextWrapping = TextWrapping.Wrap,
                                Margin = new Thickness(0, 5, 0, 0)
                            };
                            Children.Add(bugsTextBlock);
                            foreach (string bug in bugs)
                            {
                                TextBlock bugTextBlock = new TextBlock
                                {
                                    Text = "- " + bug,
                                    FontSize = 12,
                                    Foreground = Brushes.Red,

                                    TextWrapping = TextWrapping.Wrap,
                                    Margin = new Thickness(20, 0, 0, 0)
                                };
                                Children.Add(bugTextBlock);
                            }
                        }

                        //Examples
                        if (examples.Any())
                        {
                            TextBlock examplesTextBlock = new TextBlock
                            {
                                Text = "Examples :",
                                FontSize = 12,
                                Foreground = Brushes.Black,
                                FontWeight = FontWeights.Bold,

                                TextWrapping = TextWrapping.Wrap,
                                Margin = new Thickness(0, 5, 0, 0)
                            };
                            Children.Add(examplesTextBlock);
                            for (int i = 0; i < examples.Count(); i++)
                            {
                                TextBlock exampleDescTextBlock = new TextBlock
                                {
                                    Text = "Example " + (i + 1).ToString() + " : " + examplesDesc[i],
                                    FontSize = 12,
                                    Foreground = Brushes.Black,

                                    TextWrapping = TextWrapping.Wrap,
                                    Margin = new Thickness(20, 10, 0, 0)
                                };
                                Children.Add(exampleDescTextBlock);

                                TextBox exampleTextBlock = new TextBox
                                {
                                    Text = examples[i],
                                    FontSize = 12,
                                    Foreground = Brushes.DarkOrange,

                                    TextWrapping = TextWrapping.Wrap,
                                    Margin = new Thickness(20, 0, 0, 0),
                                    IsReadOnly = true
                                };
                                Children.Add(exampleTextBlock);


                            }
                        }
                        Border border = new Border()
                        {
                            BorderThickness = new Thickness(0, 1, 0, 1),
                            Height = 50,
                            Width = 300,
                            HorizontalAlignment = HorizontalAlignment.Center
                        };
                        Children.Add(border);
                    }
                }
                else
                {
                    Children.Add(nameTextBlock);
                    SetUpNoEntryPanel();
                }
            }
            catch
            {
                Children.Add(nameTextBlock);
                SetUpNoDocPanel();
            }
        }

        private void SetUpNoDocPanel()
        {
            TextBlock noDocTextBlock = new TextBlock
            {
                Text = "No documentation is currently available, try restart the app",
                TextWrapping = TextWrapping.Wrap
            };
            Children.Add(noDocTextBlock);
        }

        private void SetUpNoEntryPanel()
        {
            TextBlock missingEntryTextBlock = new TextBlock
            {
                Text = "This node seems to not exist in the documentation, no info was able to be generated",
                TextWrapping = TextWrapping.Wrap
            };
            Children.Add(missingEntryTextBlock);
            //TODO add a link to help improve the doc
        }

    }

    public class LinkButton : Button
    {
        readonly string Link;

        public LinkButton(string link, string content)
        {
            Link = link;
            Content = content;
            Click += LinkButton_Click;
            Background = Brushes.Transparent;
            BorderBrush = Brushes.Transparent;
            Foreground = Brushes.Blue;
            FontSize = 12;
            HorizontalAlignment = HorizontalAlignment.Left;
            Margin = new Thickness(15, 0, 0, 0);
        }

        private void LinkButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Link);
        }
    }



}
