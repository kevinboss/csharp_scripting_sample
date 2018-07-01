using System;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace basic_roslyn
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var globals = new Globals {X = 1, Y = 2};
            const string script = "X+Y";
            Console.WriteLine(await CSharpScript.EvaluateAsync<int>(script, globals: globals));

            Console.ReadLine();
        }
    }

    public class Globals
    {
        public int X;
        public int Y;
    }
}