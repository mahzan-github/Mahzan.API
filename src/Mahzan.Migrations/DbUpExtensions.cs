using System;
using System.Reflection;
using DbUp.Builder;
using DbUp.Engine;
using DbUp.Support;

namespace Mahzan.Migrations
{
    internal class ExecutionType
    {
        public string NamespacePrefix { get; }

        private ExecutionType(string namespacePrefix)
        {
            NamespacePrefix = namespacePrefix;
        }

        internal static readonly ExecutionType Migration = new ExecutionType("Mahzan.Migrations.Scripts.Migration.");
        internal static readonly ExecutionType Data = new ExecutionType("Mahzan.Migrations.Scripts.Data.");
        internal static readonly ExecutionType TestData = new ExecutionType("Mahzan.Migrations.Scripts.TestData.");
        internal static readonly ExecutionType Util = new ExecutionType("Mahzan.Migrations.Scripts.Util.");
    }

    public enum RepeateableScriptType
    {
        Data,
        Util,
        TestData
    }

    public static class DbUpExtensions
    {
        private static Assembly GetAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }

        private static bool IsScriptInNamespace(string prefix, string scriptName)
        {
            return scriptName.StartsWith(prefix);
        }

        private static string SimplifyName(string original, string prefix)
        {
            return original.Remove(0, prefix.Length);
        }

        public static UpgradeEngineBuilder WithAllScripts(this UpgradeEngineBuilder engine)
        {
            return engine.WithScriptsEmbeddedInAssembly(
              GetAssembly()
            );
        }

        public static UpgradeEngineBuilder WithSelectedScripts(this UpgradeEngineBuilder engine, RepeateableScriptType type)
        {
            return WithSelectedScripts(engine, type, String.Empty);
        }

        public static UpgradeEngineBuilder WithSelectedScripts(this UpgradeEngineBuilder engine, RepeateableScriptType type, string scriptNameFilter)
        {
            string prefix;

            switch (type)
            {
                case RepeateableScriptType.Data:
                    prefix = ExecutionType.Data.NamespacePrefix;
                    break;
                case RepeateableScriptType.TestData:
                    prefix = ExecutionType.TestData.NamespacePrefix;
                    break;
                case RepeateableScriptType.Util:
                    prefix = ExecutionType.Util.NamespacePrefix;
                    break;
                default:
                    throw new NotImplementedException("Script type not implemented");
            }

            return engine.WithScriptsEmbeddedInAssembly(
              GetAssembly(),
              currentScriptName =>
              {
                  if (!IsScriptInNamespace(prefix, currentScriptName))
                  {
                      return false;
                  }

                  if (String.IsNullOrEmpty(scriptNameFilter))
                  {
                      return true;
                  }

                  var simplifiedName = SimplifyName(currentScriptName, prefix);
                  return simplifiedName.Contains(scriptNameFilter, StringComparison.InvariantCultureIgnoreCase);
              },
              new SqlScriptOptions
              {
                  ScriptType = ScriptType.RunAlways
              }
            );
        }

        public static UpgradeEngineBuilder WithMigrationScripts(this UpgradeEngineBuilder engine)
        {
            return engine.WithScriptsEmbeddedInAssembly(
              GetAssembly(),
              currentScriptName => IsScriptInNamespace(ExecutionType.Migration.NamespacePrefix, currentScriptName)
            );
        }
    }
}
