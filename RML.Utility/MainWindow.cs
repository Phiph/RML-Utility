using RML.Utility.Models;
using System;
using System.Windows.Forms;
 

namespace RML.Utility
{
    public partial class MainWindow : Form
    {
        public CommandExecutor commandExecutor { get; set; }

        private AuthTypeListItems authlistItems { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            commandExecutor = new CommandExecutor();
            authlistItems = new AuthTypeListItems();
            listBox1.DataSource = commandExecutor.rmlOutput;
            comboAuthType.DataSource = authlistItems;
            comboAuthType.DisplayMember = "Text";
            comboAuthType.SelectedIndex = 0;
        }

        private void listBox1_DataSourceChanged(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void btnStartProcess_Click(object sender, EventArgs e)
        {
            commandExecutor.RunTrace(new CommandArgs
            {
                Database = txtDatabase.Text,
                Server = txtServer.Text,
                File = txtFile.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                UseWindowsAuthentication = (comboAuthType.SelectedItem as ComboBoxItem).Value
            }); 
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            commandExecutor.RunTrace(new CommandArgs
            {
                IsHelp = true
            }); 
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = openFileDialog1.FileName;
                    txtFile.Text = filePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

      
    }
}
