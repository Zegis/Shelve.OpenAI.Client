using Shelve.OpenAi.Client.models;

namespace Shelve.OpenAi.Client.requests
{
    public class CompletionsRequest
    {
        public string model { get; set; }
        public List<Message> messages { get; set; }
        public List<Tool> tools { get; set; }

        public float temperature { get; set; } = 0;
    }
}
