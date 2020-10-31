using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.DBHelper.CommandLine
{
    [Verb("migrate", HelpText = "Run schema migrations.")]
    public class MigrateOptions : CommonOptions
    {
    }
}
