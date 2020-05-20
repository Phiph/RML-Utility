using RML.Utility;
using RML.Utility.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace RML.Utility
{
    public partial class MainWindow : Form
    {
        public CommandExecutor commandExecutor { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            commandExecutor = new CommandExecutor();
            listBox1.DataSource = commandExecutor.rmlOutput;
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            commandExecutor.RunTrace(new CommandArgs
            {Database = " ",
                Server = "localhost",
                File = @"C:\RMlTraces\BandK\Performance\RMLTrace_0_132342674876580000.xel",
                Username = "sa",
                Password = " ",
                UseWindowsAuthentication = false
            });
        }

        private void listBox1_DataSourceChanged(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
    }
}
