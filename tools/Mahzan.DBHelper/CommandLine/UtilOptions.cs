using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.DBHelper.CommandLine
{
    [Verb("util", HelpText = "Run a utility script.")]
    public class UtilOptions : CommonOptions
    {
        [Value(0, MetaName = "Script Name", Required = true, HelpText = "Name of the util script to run.")]
        public string ScriptName { get; set; }

        [Option('f', "force", Required = false, Default = false,
          HelpText = "Force execution when the script name provided matches more than one script.")]
        public bool Force { get; set; }
    }
}
