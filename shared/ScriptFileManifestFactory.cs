using System.IO;

namespace shared
{
    public static class ScriptFileManifestFactory
    {
        public static ScriptFileManifest Create(string path)
        {
            var content = File.ReadAllText(path);
            return new ScriptFileManifest(content, path);
        }
    }
}
