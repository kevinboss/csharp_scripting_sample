namespace shared
{
    public class ScriptFileManifest
    {
        internal ScriptFileManifest(string content, string path)
        {
            ScriptContent = content;
            Path = path;
        }
        public string ScriptContent { get; private set; }
        public string Path { get; private set; }
    }
}
