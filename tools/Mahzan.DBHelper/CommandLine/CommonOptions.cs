using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.DBHelper.CommandLine
{
    public class CommonOptions
    {
        [Option("production", Required = false, Default = false, HelpText = "Enables development mode (disables AWS Secrets Manager).")]
        public bool ProductionMode { get; set; }
    }
}
