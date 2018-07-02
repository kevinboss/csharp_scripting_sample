namespace shared
{
    public class ScriptFileManifest
    {
        internal ScriptFileManifest(string path)
        {
            Path = path;
        }
        public string Path { get; }
    }
}
