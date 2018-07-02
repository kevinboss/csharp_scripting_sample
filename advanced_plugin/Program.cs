using System;
using System.IO;
using System.Linq;
using System.Text;
using shared;

namespace advanced_plugin
{
    class Program
    {
        private const string PluginFileEnding = ".csx";
        private const string PluginDirectory = "./Validators/";

        static void Main(string[] args)
        {
            var scriptExecutor = new ScriptExecutor();
            scriptExecutor.AddReferences(typeof(ValidationResult).Assembly);

            while (true)
            {
                Console.WriteLine("Press Enter to evaluate.");
                Console.ReadLine();

                var package = new Package
                {
                    Destination = "Switzerland",
                    Weight = 10,
                    Cost = 100
                };

                var directory = new DirectoryInfo(PluginDirectory);
                var files = directory.GetFiles().Where(e => e.Name.EndsWith(PluginFileEnding));
                var validationResult =
                    files.Select(file =>
                            scriptExecutor.Execute<ValidationResult, Package>(file.FullName, package))
                        .ToList();

                var sb = new StringBuilder();
                if (validationResult.All(e => e.IsValid))
                {
                    sb.AppendLine("No validation errors.");
                }
                else
                {
                    sb.AppendLine("Errors: ");
                    foreach (var result in validationResult.Where(e => !e.IsValid))
                    {
                        sb.AppendLine(result.ErrorText);
                    }
                }
                Console.WriteLine(sb);
            }

            // ReSharper disable once FunctionNeverReturns
        }
    }

    public class Package
    {
        public string Destination;
        public int Weight;
        public int Cost;
    }

    public class ValidationResult
    {
        public string ErrorText;
        public bool IsValid;
    }
}