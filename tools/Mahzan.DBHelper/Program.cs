using CommandLine;
using Mahzan.DBHelper.CommandLine;
using System;

namespace Mahzan.DBHelper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var executor = new MahzanScriptExecutor();


            int exitCode = Parser.Default.ParseArguments<
                MigrateOptions>(args)
                  .MapResult(
                    (MigrateOptions opts) => executor.InvokeMigration(opts),
                    errs => 1);

            /*int exitCode = Parser.Default.ParseArguments<
                MigrateOptions,
                DataOptions,
                UtilOptions,
                ListOptions
              >(args)
              .MapResult(
                (MigrateOptions opts) => executor.InvokeMigration(opts),
                (DataOptions opts) => executor.InvokeDataScript(opts),
                (UtilOptions opts) => executor.InvokeUtilScript(opts),
                (ListOptions opts) => executor.InvokeListScripts(opts),
                errs => 1);*/

            Environment.ExitCode = exitCode;
        }
    }
}
