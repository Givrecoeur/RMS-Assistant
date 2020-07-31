using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace RMS_Assistant
{
    public class RMSFileManager
    {
        public string FileDirectory;
        readonly RMSParser Parser; 

        public RMSFileManager(MainWindow ui)
        {
            FileDirectory = null;
            Parser = new RMSParser(ui);
        }

        public bool WarnForSave(RMSRoot root)
        {
            bool doContinue = true;
            if (root.NeedSave)
            {
                const string message = "You have some unsaved work, would you like to save it before closing current project ?";
                const string caption = "Warning : unsaved changes";
                MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == DialogResult.Yes)
                {
                    if (SaveFile(root))
                    {
                        doContinue = true;
                    }
                    else
                    {
                        doContinue = false;
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    doContinue = false;
                }
            }
            return doContinue;
        }

        public bool OpenFile(RMSRoot root)
        {
            bool isFileOpened = false;
            if (WarnForSave(root))
            {
                OpenFileDialog openDialog = new OpenFileDialog { Filter = "rms files (*.rms)|*.rms", RestoreDirectory = true };

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    FileDirectory = String.Copy(openDialog.FileName);
                }
                else
                {
                    return isFileOpened;
                }

                if (ReadFile(FileDirectory))
                {
                    isFileOpened = true;
                }
                else
                {
                    isFileOpened = false;
                }
            }
            return isFileOpened;
        }


        public bool SaveFile(RMSRoot root, bool saveAs = false)
        {
            bool isFileSaved = false;
            if (FileDirectory == null || saveAs)
            {
                SaveFileDialog saveDialog = new SaveFileDialog { Filter = "rms files (*.rms)|*.rms", RestoreDirectory = true };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    FileDirectory = String.Copy(saveDialog.FileName);
                }
                else
                {
                    return isFileSaved;
                }
            }

            using (StreamWriter sw = new StreamWriter(FileDirectory))
            {
                if (WriteFile(root, sw))
                {
                    isFileSaved = true; ;
                }
            }
            return isFileSaved;

        }


        public bool ReadFile(string pathtofile)
        {
            RMSRoot loadedTree;
            loadedTree = Parser.BuildTree(pathtofile);
            if (loadedTree != null)
            {
                return true;
            }
            return false;
        }

        private bool WriteFile(RMSRoot root, StreamWriter file)
        {
            try
            {
                foreach (RMSNode node in root.Children)
                {
                    node.Print(file, 0);
                }
                return true;
            }
            catch
            {
                //TODO return a maessage to say saving the file failed
                return false;
            }
        }
    }
}
