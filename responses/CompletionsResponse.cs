using Shelve.OpenAi.Client.models;
using System.Text.Json.Serialization;

namespace Shelve.OpenAi.Client.responses
{
    public class CompletionResponse
    {
        private DateTime _created;

        public string id { get; set; }
        [JsonPropertyName("object")]
        public string objectName { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public List<Choices> choices { get; set; }
        public Usage usage { get; set; }
    }

    public class Choices
    {
        public int index { get; set; }

        [JsonPropertyName("finish_reason")]
        public string finishReason { get; set; }
        public Message message { get; set; }
    }

    public class Usage
    {
        [JsonPropertyName("prompt_tokens")]
        public int promptTokens { get; set; }

        [JsonPropertyName("complation_tokens")]
        public int completionTokens { get; set; }

        [JsonPropertyName("total_tokens")]
        public int totalTokens { get; set; }
    }
}
