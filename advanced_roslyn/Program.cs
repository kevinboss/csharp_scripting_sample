using System;
using shared;

namespace advanced_roslyn
{
	class Program
	{
		private const string ScriptFilePath = "helloworld.csx";

		static void Main(string[] args)
		{
			var globals = new Globals {X = 1, Y = 2};
			var manifest = ScriptFileManifestFactory.Create(ScriptFilePath);
			var result = MyScriptExecutor.Execute<int, Globals>(manifest, globals);
			Console.WriteLine(result);

			Console.ReadLine();
		}
	}

	public class Globals
	{
		public int X;
		public int Y;
	}
}