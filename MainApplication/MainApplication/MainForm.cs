using System;
using System.IO;
using System.Windows.Forms;

namespace MainApplication
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //lets allow the form to accept drag and drop event
            this.AllowDrop = true;
        }

        //lets implement the events
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            //lets set the effect
            e.Effect = DragDropEffects.Copy;
        }

        //now lets process the data being dropped
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            String path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

            //check if it is a directory/folder
            if (File.GetAttributes(path) == FileAttributes.Directory)
            {
                //lets read all files in the directory
                foreach (String file in Directory.GetFiles(path))
                {
                    CopyFileToTextBox(file);
                }
            }
            else
                CopyFileToTextBox(path);
        }

        //ENHANCEMENT =======> move this to a single method
        private void CopyFileToTextBox(string filePath)
        {
            if (String.IsNullOrWhiteSpace(txtInput.Text))
                txtInput.AppendText(File.ReadAllText(filePath));
            else
                txtInput.AppendText(Environment.NewLine + File.ReadAllText(filePath));
        }
    }
}
