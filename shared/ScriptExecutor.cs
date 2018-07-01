using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace shared
{
    public static class ScriptExecutor
    {
        public static TReturnType Execute<TReturnType, TGlobalsType>(ScriptFileManifest script, TGlobalsType globals)
        {
            var scriptContent = GetScriptContent(script);

            var compilation = CreateCompilation<TGlobalsType>(scriptContent);

            var rawAssemblyResult = RawAssembly(compilation);

            var assembly = Assembly.Load(rawAssemblyResult.RawAssembly, rawAssemblyResult.RawSymbol);

            var entryPoint = GetEntryPointFromAssembly(assembly);

            var result = ExecuteEntryPoint(globals, entryPoint);

            if (result is TReturnType castedResult)
            {
                return castedResult;
            }

            return default(TReturnType);
        }

        private static object ExecuteEntryPoint<TGlobalsType>(TGlobalsType globals, MethodBase method)
        {
            var submissionStates = new object[2];
            submissionStates[0] = globals;
            var task = (method.Invoke(null, new object[] {submissionStates}) as Task<object>);
            task?.Wait();
            var taskResult = task?.Result;
            return taskResult;
        }

        private static MethodInfo GetEntryPointFromAssembly(Assembly assembly)
        {
            var type = assembly.GetType("Submission#0");
            var method = type.GetMethod("<Factory>", BindingFlags.Static | BindingFlags.Public);
            return method;
        }

        private static string GetScriptContent(ScriptFileManifest script)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"#line 1 \"{script.Path}\"");
            sb.AppendLine(script.ScriptContent);
            var scriptContent = sb.ToString();
            return scriptContent;
        }

        private static RawAssemblyResult RawAssembly(Compilation compilation)
        {
            byte[] rawAssembly;
            byte[] rawSymbol;
            using (var assemblyStream = new MemoryStream())
            {
                using (var symbolStream = new MemoryStream())
                {
                    var emitOptions = new EmitOptions(false, DebugInformationFormat.PortablePdb);
                    var result = compilation.Emit(assemblyStream, symbolStream, options: emitOptions);
                    if (!result.Success)
                    {
                        var errors = string.Join(Environment.NewLine, result.Diagnostics.Select(x => x));
                        Console.WriteLine(errors);
                    }

                    rawAssembly = assemblyStream.ToArray();
                    rawSymbol = symbolStream.ToArray();
                }
            }

            return new RawAssemblyResult
            {
                RawAssembly = rawAssembly,
                RawSymbol = rawSymbol
            };
        }

        private static Compilation CreateCompilation<TGlobalsType>(string scriptContent)
        {
            var options = Microsoft.CodeAnalysis.Scripting.ScriptOptions.Default;
            var roslynScript = CSharpScript.Create(scriptContent, options, typeof(TGlobalsType));
            var compilation = roslynScript.GetCompilation();

            compilation = compilation.WithOptions(compilation.Options
                .WithOptimizationLevel(OptimizationLevel.Debug)
                .WithOutputKind(OutputKind.DynamicallyLinkedLibrary));
            return compilation;
        }

        private class RawAssemblyResult
        {
            public byte[] RawAssembly;
            public byte[] RawSymbol;
        }
    }
}