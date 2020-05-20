using RML.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RML.Utility.Models
{
    public class CommandArgs
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public bool UseWindowsAuthentication { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string File { get; set; }
        public string AdditionalArgs { get; set; }
        public string ToCommandArgs()
        {
            var arguments = new Dictionary<string, string>
            {
                {"-S", $"{Server}"},
                {"-d", $"{Database}"},
                {"-I", $"{File}"}

            };
            if (!UseWindowsAuthentication)
            {
                arguments.Add("-U", $"{Username}");
                arguments.Add("-P", $"{Password}");
            }

            return string.Concat(arguments.ToDebugString(), AdditionalArgs);

        }

    }
}
