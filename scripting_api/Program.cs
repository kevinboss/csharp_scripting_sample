using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace scripting_api
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args) {
            var scriptCode = File.ReadAllText("../scripting_api/hello_world/main.csx");
            var scriptOptions = ScriptOptions.Default.
                WithImports("System").
                WithReferences(typeof(System.Console).GetTypeInfo().Assembly);
            
            // Run
            var scriptResult = await CSharpScript.RunAsync(scriptCode, scriptOptions);
        }
    }
}
