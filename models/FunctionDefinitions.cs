namespace Shelve.OpenAi.Client.models
{
    public class Tool
    {
        public string type => "function";

        public FunctionDefinition function { get; set; }
    }

    public class FunctionDefinition
    {
        public string name { get; set; }
        public string description { get; set; }
        public FunctionParameterObject parameters { get; set; }
    }

    public class FunctionParameterObject
    {
        public string type { get; set; }
        public Dictionary<string, ObjectProperty> properties { get; set; }
        public List<string> required { get; set; }
    }

    public class ObjectProperty
    {
        public string type { get; set; }
        public string description { get; set; }
    }

    public class FunctionCall
    {
        public string id { get; set; }
        public string type { get; set; }
        public CalledFunction function { get; set; }
    }

    public class CalledFunction
    {
        public string name { get; set; }
        public string arguments { get; set; }
    }
}
