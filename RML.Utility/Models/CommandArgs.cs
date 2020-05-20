using RML.Utility.Extensions;
using System.Collections.Generic;

namespace RML.Utility.Models
{
    public class CommandArgs
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public bool UseWindowsAuthentication { get; set; } = true;
        public string Username { get; set; }
        public string Password { get; set; }
        public string File { get; set; }
        public string AdditionalArgs { get; set; }
        public bool IsHelp { get; set; } = false;
        
        
        public string ToCommandArgs()
        {
            if (IsHelp)
            {
                return "-h";
            }

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
