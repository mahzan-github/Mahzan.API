using DbUp;
using Mahzan.DBHelper.CommandLine;
using Mahzan.Migrations;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ConfigurationExtensions = Microsoft.Extensions.Configuration.ConfigurationExtensions;

namespace Mahzan.DBHelper
{

    public class MahzanScriptExecutor
    {
        public int InvokeListScripts(ListOptions opts)
        {
            var configuration = LoadConfiguration(opts.ProductionMode);

            var upgrader = DeployChanges.To
              .PostgresqlDatabase(ConfigurationExtensions.GetConnectionString(configuration, "Mahzan"))
              .WithAllScripts()
              .LogToConsole()
              .Build();

            var discoveredScripts = upgrader.GetDiscoveredScripts();

            Write("Discovered scripts:");
            foreach (var scriptItem in discoveredScripts)
            {
                Write(" - " + scriptItem.Name);
            }

            var executedScripts = upgrader.GetExecutedScripts();

            Write("Already executed scripts:");
            foreach (var scriptItem in executedScripts)
            {
                Write(" - " + scriptItem);
            }

            var executedNotDiscovered = upgrader.GetExecutedButNotDiscoveredScripts();

            Write("Missing executed scripts:");
            foreach (var scriptItem in executedNotDiscovered)
            {
                Write(" - " + scriptItem);
            }

            return 0;
        }

        public int InvokeMigration(MigrateOptions options)
        {
            var configuration = LoadConfiguration(options.ProductionMode);

            var connectionString = ConfigurationExtensions.GetConnectionString(configuration, "RiskMonitoringEngine");

            var upgrader = DeployChanges.To
              .PostgresqlDatabase(connectionString)
              .WithMigrationScripts()
              .LogToConsole()
              .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                WriteError(result.Error);
                return -1;
            }

            WriteSuccess("Success!");
            return 0;
        }

        public int InvokeDataScript(DataOptions options)
        {
            return InvokeRepeatableScript(options.ScriptName, options.Force, RepeateableScriptType.Data, options.ProductionMode);
        }

        public int InvokeUtilScript(UtilOptions options)
        {
            return InvokeRepeatableScript(options.ScriptName, options.Force, RepeateableScriptType.Util, options.ProductionMode);
        }

        private int InvokeRepeatableScript(string scriptNameFilter, bool force, RepeateableScriptType scriptType, bool productionMode)
        {
            var configuration = LoadConfiguration(productionMode);

            var upgrader = DeployChanges.To
              .PostgresqlDatabase(ConfigurationExtensions.GetConnectionString(configuration, "RiskMonitoringEngine"))
              .WithSelectedScripts(
                scriptType,
                scriptNameFilter
              )
              .LogToConsole()
              .Build();

            var scriptsToExecute = upgrader.GetScriptsToExecute();
            if (scriptsToExecute.Count == 0)
            {
                WriteError("No scripts matched with the name provided");
                return -2;
            }

            if (scriptsToExecute.Count > 1 && !force)
            {
                var matchedScripts = scriptsToExecute.Select(script => script.Name).ToArray();
                WriteError($"More than one script matched with the name provided: \n" +
                           string.Join("\n", matchedScripts) +
                           $"\n\nUse the -f flag to apply them both.");
                return -2;
            }

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                WriteError(result.Error);
                return -1;
            }

            WriteSuccess("Success!");
            return 0;
        }

        private void WriteWarning(object text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private void WriteSuccess(object text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private void WriteError(object text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private void Write(object text)
        {
            Console.WriteLine(text);
        }

        private IConfiguration LoadConfiguration(bool productionMode)
        {
            var currentAssembly = Assembly.GetAssembly(typeof(MahzanScriptExecutor));
            var stream = currentAssembly.GetManifestResourceStream("Conekta.Risk.Monitoring.Engine.Tools.DBHelper.appsettings.json");

            var configurationBuilder = FileConfigurationExtensions.SetBasePath(new ConfigurationBuilder(), Directory.GetCurrentDirectory())
              .AddJsonStream(stream);

            if (!productionMode)
            {
                configurationBuilder.AddUserSecrets<MahzanScriptExecutor>();
            }
            else
            {
                /*configurationBuilder.Add(new SecretsManagerConfigurationSource(
                    secretId: Environment.GetEnvironmentVariable("AWS_SECRETS_MANAGER_NAME") ??
                              throw new ArgumentException("AWS_SECRETS_MANAGER_NAME environment variable must be set"),
                    regionEndpoint: Environment.GetEnvironmentVariable("AWS_SECRETS_MANAGER_REGION") ??
                                    throw new ArgumentException(
                                        "AWS_SECRETS_MANAGER_REGION environment variable must be set"),
                    versionStage: Environment.GetEnvironmentVariable("AWS_SECRETS_MANAGER_VERSION_STAGE")
                    ));*/
            }

            configurationBuilder.AddEnvironmentVariables();

            return configurationBuilder.Build();
        }
    }
}
