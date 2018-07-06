namespace shared
{
    public class ScriptFileManifest
    {
        internal ScriptFileManifest(string path, string content)
        {
            Path = path;
            ScriptContent = content;
        }
        public string Path { get; }
        public string ScriptContent { get; }
    }
}
