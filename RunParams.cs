using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TaskTool
{
    public class RunParams
    {
        public string ConfigFullPath;

        public bool ShowUi { get; set; }

        public bool Wait { get; set; }

        public static RunParams FromConsoleArgs(string[] args)
        {
            //  no parameters, launch the UI
            if (args.Length == 0)
            {
                return new RunParams()
                {
                    ShowUi = true,
                    Wait = true,
                    ConfigFullPath = null
                };
            }
            else
            {
                //  always assume the first parameter is a config path
                var result = new RunParams { ConfigFullPath = Path.GetFullPath(args[0]) };

                //  read pairs and set values
                for (int i = 1; i < args.Length; i += 2)
                {
                    var key = args[i];
                    var value = args[i + 1];

                    if (!properties.TryGetValue(key, out var prop))
                        throw new Exception($"Invalid RunParams property '{key}'");

                    prop.SetValue(result, Convert.ChangeType(value, prop.PropertyType));
                }

                return result;
            }
        }

        public static string GetHelp()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Parameters: configFullPath [options]");
            sb.AppendLine("Available options:");

            foreach (var pair in properties.Values)
                sb.AppendLine($"\t/{pair.Name}\t{pair.PropertyType.Name}");

            return sb.ToString();
        }

        private static readonly Dictionary<string, PropertyInfo> properties = typeof(RunParams)
            .GetProperties()
            .ToDictionary(x => x.Name, StringComparer.InvariantCultureIgnoreCase);
    }
}
