using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.DBHelper.CommandLine
{
    [Verb("list", HelpText = "Lists all known scripts")]
    public class ListOptions : CommonOptions
    {
    }
}
