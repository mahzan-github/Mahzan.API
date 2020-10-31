using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.DBHelper.CommandLine
{
    [Verb("data", HelpText = "Run data seeding scripts.")]
    public class DataOptions : CommonOptions
    {
        [Value(0, MetaName = "Script Name", Required = true, HelpText = "Name of the script to run.")]
        public string ScriptName { get; set; }

        [Option('f', "force", Required = false, Default = false, HelpText = "Force execution when the script name provided matches more than one script.")]
        public bool Force { get; set; }
    }
}
