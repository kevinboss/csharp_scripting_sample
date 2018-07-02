using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Dotnet.Script.Core;
using Dotnet.Script.DependencyModel.Logging;
using Dotnet.Script.DependencyModel.Runtime;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Text;

namespace shared
{
    public class ScriptExecutor
    {
        private static bool LogVerbosityDebug = false;
        private static OptimizationLevel OptimizationLevel = Microsoft.CodeAnalysis.OptimizationLevel.Release;
        private Assembly[] _references = new Assembly[0];

        static ScriptExecutor()
        {
            var isDebug = false;
            Debug.Assert(isDebug = true);

            if (!isDebug)
            {
                return;
            }

            OptimizationLevel = OptimizationLevel.Debug;
        }

        public TReturnType Execute<TReturnType, TGlobalsType>(string scriptPath, TGlobalsType globals)
        {
            var task = RunScript<TReturnType, TGlobalsType>(scriptPath, globals);
            task.Wait();
            return task.Result;
        }

        public ScriptExecutor AddReferences(params Assembly[] references)
        {
            _references = references;
            return this;
        }

        private async Task<TReturnType> RunScript<TReturnType, TGlobalsType>(string file,
            TGlobalsType globals)
        {
            if (!File.Exists(file))
            {
                throw new Exception($"Couldn't find file '{file}'");
            }

            var absoluteFilePath = Path.IsPathRooted(file) ? file : Path.Combine(Directory.GetCurrentDirectory(), file);
            var directory = Path.GetDirectoryName(absoluteFilePath);

            using (var filestream =
                new FileStream(absoluteFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var sourceText = SourceText.From(filestream);
                var context = new ScriptContext(
                    sourceText,
                    directory,
                    new List<string>(),
                    absoluteFilePath,
                    OptimizationLevel
                );

                return await Run<TReturnType, TGlobalsType>(context, globals);
            }
        }

        private Task<TReturnType> Run<TReturnType, TGlobalsType>(ScriptContext context, TGlobalsType globals)
        {
            var compiler = GetScriptCompiler();
            var runner = new ScriptRunner(compiler, compiler.Logger, ScriptConsole.Default);
            return runner.Execute<TReturnType, TGlobalsType>(context, globals);
        }

        private ScriptCompiler GetScriptCompiler()
        {
            var logger = new ScriptLogger(ScriptConsole.Default.Error, LogVerbosityDebug);
            var runtimeDependencyResolver = new RuntimeDependencyResolver(type => ((level, message) =>
            {
                switch (level)
                {
                    case LogLevel.Debug:
                        logger.Verbose(message);
                        break;
                    case LogLevel.Info:
                        logger.Log(message);
                        break;
                }
            }));

            var compiler = new ScriptCompiler(logger, runtimeDependencyResolver);
            return compiler;
        }
    }
}