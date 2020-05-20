using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RML.Utility.Models;

namespace RML.Utility
{
    public class CommandExecutor
    {
        public const string rmlExe = "ReadTrace.exe";

        public const string RMLLocation = @"C:\Program Files\Microsoft Corporation\RMLUtils\";

        private readonly BackgroundWorker Processor = new BackgroundWorker();

        public CommandExecutor()
        {
            Processor.WorkerReportsProgress = true;
            Processor.WorkerSupportsCancellation = true;
            Processor.ProgressChanged += ProcessorProgressChanged;
            Processor.DoWork += ProcessorDoWork;
            Processor.RunWorkerCompleted += Processor_RunWorkerCompleted;
        }

        public BindingList<string> rmlOutput { get; set; } = new BindingList<string>();

        public event EventHandler ProcessCompleted;

        public event EventHandler Error;
        private void Processor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProcessCompleted?.Invoke(this, e);
        }

        public void RunTrace(CommandArgs cmdArgs)
        {
            var StartInfo = new ProcessStartInfo(Path.Combine(RMLLocation, rmlExe), cmdArgs.ToCommandArgs())
            {
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process p = null;
            try
            {
                p = Process.Start(StartInfo);
            }
            catch (Exception ex)
            {
                Error?.Invoke(this, new EventArgs());
                
                return;
            }

            // Get the output
            Processor.RunWorkerAsync(p.StandardOutput);
        }

        private void ProcessorDoWork(object sender, DoWorkEventArgs e)
        {
            var StandardOutput = e.Argument as StreamReader;
            var data = StandardOutput.ReadLine();
            while (data != null)
            {
                Processor.ReportProgress(0, data);
                data = StandardOutput.ReadLine();
            }
        }

        private void ProcessorProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var data = e.UserState as string;
            if (data != null)
                rmlOutput.Add(data);
        }
    }
}