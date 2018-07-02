using System.IO;

namespace shared
{
    public static class ScriptFileManifestFactory
    {
        public static ScriptFileManifest Create(string path)
        {
            return new ScriptFileManifest(path);
        }
    }
}
