using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Ink;

namespace RMS_Assistant
{
    public class RMSParser
    {
        List<string[]> AllLines;
        int CurrentLineIndex;
        int CurrentWordIndex;
        string CurrentSection;
        readonly MainWindow UI;


        public RMSParser(MainWindow ui)
        {
            CurrentSection = "";
            UI = ui;
            CurrentWordIndex = 0;
            CurrentLineIndex = 0;
        }

        public RMSRoot BuildTree(string pathToFile)
        {
            if (!ReadRMSFile(pathToFile)) return null;

            RMSRoot root = new RMSRoot(UI);
            RMSSection playerSetup =          new RMSSection("PLAYER_SETUP", root, UI);
            RMSSection landGeneration =       new RMSSection("LAND_GENERATION", root, UI);
            RMSSection elevationGeneration =  new RMSSection("ELEVATION_GENERATION", root, UI);
            RMSSection cliffGeneration =      new RMSSection("CLIFF_GENERATION", root, UI);
            RMSSection terrainGeneration =    new RMSSection("TERRAIN_GENERATION", root, UI);
            RMSSection connectionGeneration = new RMSSection("CONNECTION_GENERATION", root, UI);
            RMSSection objectGeneration =     new RMSSection("OBJECTS_GENERATION", root, UI);


            UI.Root = root;
            UI.xamlRMSTree.ItemsSource = new BindingList<RMSRoot> { UI.Root };
            do
            {
                switch (CurrentSection)
                {
                    case "":
                        FillSection(root);
                        break;

                    case "PLAYER_SETUP":
                        FillSection(playerSetup);
                        root.Children.Add(playerSetup);
                        break;

                    case "LAND_GENERATION":
                        FillSection(landGeneration);
                        root.Children.Add(landGeneration);
                        break;

                    case "ELEVATION_GENERATION":
                        FillSection(elevationGeneration);
                        root.Children.Add(elevationGeneration);
                        break;

                    case "CLIFF_GENERATION":
                        FillSection(cliffGeneration);
                        root.Children.Add(cliffGeneration);
                        break;

                    case "TERRAIN_GENERATION":
                        FillSection(terrainGeneration);
                        root.Children.Add(terrainGeneration);
                        break;

                    case "CONNECTION_GENERATION":
                        FillSection(connectionGeneration);
                        root.Children.Add(connectionGeneration);
                        break;

                    case "OBJECTS_GENERATION":
                        FillSection(objectGeneration);
                        root.Children.Add(objectGeneration);
                        break;

                    default:
                        throw new Exception("Unknown section parsed");
                }
            } while (CurrentSection != "");
            
            AllLines = null;
            CurrentWordIndex = 0;
            CurrentLineIndex = 0;
            CurrentSection = "";

            bool useCliff = false;
            foreach (RMSNode section in root.Children)
            {
                if (section.Name == "CLIFF_GENERATION")
                {
                    useCliff = true;
                    break;
                }
            }
            root.useCliff = useCliff;

            return root; //TODO clean this : function return ROOT, but its not useful
            }

        private bool ReadRMSFile(string pathToFile)
        {
            try
            {
                AllLines = new List<string[]>();
                string[] lines = File.ReadAllLines(pathToFile);
                foreach (string line in lines)
                { 
                    AllLines.Add(line.Split(new Char[] { ' ', '\n', '\t', '\r' }));
                }
                
                return true;
        
            }
            catch
            {
                return false;
            }
        }
        
        private void FillSection(RMSNode sectionNode)
        {
            Regex isSection = new Regex("^<.*>$");
            bool sectionChanged = false;
            int nbAttributeExpected = 0;
            int indexAttribute = 0;
            RMSNode currentNode = sectionNode;
            RMSNode currentCommandParent = sectionNode;
            RMSNode lastAddedNode = sectionNode;
            int commentDepth = 0;
            string currentComment = "";
            bool isCurrentCommentInline = false;
            List<RMSCommand> pendingCommands = new List<RMSCommand>();

            while (!sectionChanged)
            {
                if (CurrentLineIndex >= AllLines.Count()) //end of file
                {
                    CurrentSection = "";
                    sectionChanged = true;
                    if (pendingCommands.Count > 0) throw new Exception("Unfinished command at end of file");
                    continue;
                }

                if (CurrentWordIndex >= AllLines[CurrentLineIndex].Count())
                {
                    CurrentLineIndex++;
                    CurrentWordIndex = 0;
                    continue;
                }
        
                string word = AllLines[CurrentLineIndex][CurrentWordIndex];
                
                //debug stuff
                /*string where;
                if (CurrentWordIndex > 10)
                {
                    string prev = AllWords[CurrentWordIndex - 1];
                    string prev2 = AllWords[CurrentWordIndex - 2];
                    string prev3 = AllWords[CurrentWordIndex - 3];
                    string prev4 = AllWords[CurrentWordIndex - 4];
                    string prev5 = AllWords[CurrentWordIndex - 5];
                    string prev6 = AllWords[CurrentWordIndex - 6];
                    string prev7 = AllWords[CurrentWordIndex - 7];
                    string prev8 = AllWords[CurrentWordIndex - 8];
                    where = prev8 + prev7 + prev6 + prev5 + prev4 + prev3 + prev2 + prev + word;
                }*/
                //end debug stuff

                if (word == "")
                {
                    CurrentWordIndex++;
                    continue;
                }
        
                if (word == "/*") //start of a comment
                {
                    commentDepth++;
                    currentComment = "";

                    if (CurrentWordIndex == 0) isCurrentCommentInline = true;
                    else isCurrentCommentInline = false;

                    CurrentWordIndex++;
                    continue;
                }
                
                else if (word == "*/") //end of a comment
                {
                    commentDepth--;
                    if (commentDepth < 0) commentDepth = 0;

                    if (isCurrentCommentInline)
                    {
                        RMSComment comment = new RMSComment(currentNode, UI)
                        {
                            Comment = currentComment
                        };
                        currentNode.AddNode(comment);
                    }
                    else
                    {
                        lastAddedNode.Comment = currentComment;
                    }
                    CurrentWordIndex++;
                    continue;
                }
                
                
                if (commentDepth > 0) //if we are in a comment
                {
                    currentComment += " " + word;
                }
                else
                {
                    if (isSection.IsMatch(word)) //start of a new section
                    {
                        sectionChanged = true;
                        char[] charsToTrim = { '<', '>' };
                        word = word.Trim(charsToTrim);
                        if (RMSNodeNameConstants.Sections.Keys.ToList().Contains(word)) CurrentSection = word;
                        else throw new Exception("Impossible section name");
                        if (pendingCommands.Count > 0) throw new Exception("Unfinished command at end of section " + CurrentSection);
                
                    }
                
                    else if (nbAttributeExpected > 0) //we are in a node and wait for its attribute(s)
                    {
                        switch (indexAttribute)
                        {
                            case 0:
                                currentNode.Attribute0 = word;
                                break;
                            case 1:
                                currentNode.Attribute1 = word;
                                break;
                            case 2:
                                currentNode.Attribute2 = word;
                                break;
                            case 3:
                                currentNode.Attribute3 = word;
                                break;
                            default:
                                throw new Exception("Impossible index for an attribute");
                        }
                        indexAttribute++;
                        if (indexAttribute == nbAttributeExpected) //all attributes have been read
                        {
                            indexAttribute = 0;
                            nbAttributeExpected = 0;
                            if (currentNode is RMSProperty || currentNode is RMSDefine || currentNode is RMSConstant || currentNode is RMSCommand || currentNode is RMSInclude)
                            {
                                currentNode = currentNode.Parent; //once we have their attribute, these kind of node are completed (or need a "{" to begin in RMSCommand case)
                            }
                        }
                    }
                
                    else //we expect a new node
                    {
                        Dictionary<string, int> availableProperties;
                        Dictionary<string, int> availableCommands;
                        if (currentNode is RMSCommand)
                        {
                            availableProperties = RMSNodeNameConstants.FromNameGetDictProperty(currentNode.Name);
                            availableCommands = RMSNodeNameConstants.FromNameGetDictCommand(currentNode.Name);
                        }
                        else
                        {
                            availableProperties = RMSNodeNameConstants.FromNameGetDictProperty(currentNode.GetRelevantParent(currentNode).Name);
                            availableCommands = RMSNodeNameConstants.FromNameGetDictCommand(currentNode.GetRelevantParent(currentNode).Name);
                        }

                        if (word == "#define")
                        {
                            RMSDefine newDefine = new RMSDefine(currentNode, UI);
                            currentNode.AddNode(newDefine);
                            currentNode = newDefine;
                            lastAddedNode = newDefine;
                            nbAttributeExpected = 1;
                        }

                        else if (word == "#const")
                        {
                            RMSConstant newConstant = new RMSConstant(currentNode, UI);
                            currentNode.AddNode(newConstant);
                            currentNode = newConstant;
                            lastAddedNode = newConstant;
                            nbAttributeExpected = 2;
                        }

                        else if (word == "if")
                        {
                            RMSConditional newConditional = new RMSConditional(currentNode, UI);
                            currentNode.AddNode(newConditional);
                            RMSCondition newCondition = new RMSCondition(word, newConditional, UI);
                            newConditional.AddNode(newCondition);
                            currentNode = newCondition;
                            lastAddedNode = newCondition;
                            nbAttributeExpected = 1;
                        }

                        else if (word == "elseif")
                        {
                            if (currentNode is RMSCondition && (currentNode.Name == "if" || currentNode.Name == "elseif"))
                            {
                                RMSCondition newCondition = new RMSCondition(word, currentNode.Parent, UI);
                                currentNode.Parent.AddNode(newCondition);
                                currentNode = newCondition;
                                lastAddedNode = newCondition;
                                nbAttributeExpected = 1;
                            }
                            else throw new Exception("Incorrectly placed elseif");
                        }

                        else if (word == "else")
                        {
                            if (currentNode is RMSCondition)
                            {
                                RMSCondition newCondition = new RMSCondition(word, currentNode.Parent, UI);
                                currentNode.Parent.AddNode(newCondition);
                                currentNode = newCondition;
                                lastAddedNode = newCondition;
                            }
                            else throw new Exception("Incorrectly placed else");
                        }

                        else if (word == "endif")
                        {
                            if (currentNode is RMSCondition)
                            {
                                currentNode = currentNode.Parent.Parent;
                            }
                            else throw new Exception("Incorrectly placed endif");
                        }

                        else if (word == "start_random")
                        {
                            RMSRandom newRandom = new RMSRandom(currentNode, UI);
                            currentNode.AddNode(newRandom);
                            currentNode = newRandom;
                            lastAddedNode = newRandom;
                        }

                        else if (word == "percent_chance")
                        {
                            RMSNode parent;
                            if (currentNode is RMSRandom)
                            {
                                parent = currentNode;
                            }
                            else if (currentNode is RMSWeigth)
                            {
                                parent = currentNode.Parent;
                            }
                            else
                            {
                                throw new Exception("Impossible position for a randdom weigth");
                            }
                            RMSWeigth newWeigth = new RMSWeigth(parent, UI);
                            parent.AddNode(newWeigth);
                            currentNode = newWeigth;
                            lastAddedNode = newWeigth;
                            nbAttributeExpected = 1;

                        }

                        else if (word == "end_random")
                        {
                            if (currentNode is RMSWeigth)
                            {
                                currentNode = currentNode.Parent.Parent;
                            }
                            else throw new Exception("Incorrectly placed end_random");
                        }

                        else if (word == "#include" || word == "#include_drs"  || word == "#includeXS")
                        {
                            RMSInclude newInclude = new RMSInclude(word, currentNode, UI);
                            currentNode.AddNode(newInclude);
                            currentNode = newInclude;
                            lastAddedNode = newInclude;
                            nbAttributeExpected = 1;
                        }

                        else if (availableCommands != null && availableCommands.Keys.Contains(word))
                        {
                            RMSCommand newCommand = new RMSCommand(word, currentNode, UI);
                            if (pendingCommands.Count > 0)
                            {
                                if (pendingCommands[pendingCommands.Count - 1].Name == word || RMSNodeNameConstants.CommandsConnection.Keys.ToList().Contains(word))
                                {
                                    pendingCommands.Add(newCommand);
                                }
                                else throw new Exception("Declared multiple commands, but no core");
                            }
                            else
                            {
                                pendingCommands.Add(newCommand);
                            }
                            nbAttributeExpected = availableCommands[word];
                            if (nbAttributeExpected > 0)
                            {
                                currentNode = newCommand;
                            }
                            lastAddedNode = currentNode;
                        }
                        else if (availableProperties != null && availableProperties.Keys.Contains(word))
                        {
                            RMSProperty newProperty = new RMSProperty(word, currentNode, UI);
                            currentNode.AddNode(newProperty);
                            nbAttributeExpected = availableProperties[word];
                            if (nbAttributeExpected > 0)
                            {
                                currentNode = newProperty;
                            }
                            lastAddedNode = currentNode;
                        }
                        else if (word == "{")
                        {
                            if (pendingCommands.Count > 0)
                            {
                                currentCommandParent = currentNode;
                                currentNode = pendingCommands[pendingCommands.Count - 1];
                                lastAddedNode = pendingCommands[pendingCommands.Count - 1];
                            }
                            else throw new Exception("Trying to open a command but none was declared");
                        }
                        else if (word == "}")
                        {
                            List<RMSNode> allChildren = currentNode.Children.ToList();
                            for (int i = 0; i<(pendingCommands.Count - 1); i++) 
                            {
                                foreach (RMSNode child in allChildren)
                                {
                                    pendingCommands[i].AddNode(child.Clone());
                                }
                                pendingCommands[i].Parent.AddNode(pendingCommands[i]);
                            }
                            currentNode.Parent.AddNode(currentNode);
                            currentNode = currentCommandParent;
                            pendingCommands.Clear();
                        }
                        else throw new Exception("Unknown Node declaration");
                    }
                }
                CurrentWordIndex++;
            }
        }
    }
}
