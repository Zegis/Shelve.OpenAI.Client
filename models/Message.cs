using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shelve.OpenAi.Client.models
{
    public class Message
    {
        public virtual string role { get; set; }

        [JsonConverter(typeof(MessageContentConverter))]
        public MessageContent content { get; set; }

        public List<FunctionCall> tool_calls { get; set; }
    }

    public class MessageContentConverter : JsonConverter<MessageContent>
    {
        public override MessageContent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var text = reader.GetString();
                return new MessageContent(text);
            }
            else if (reader.TokenType == JsonTokenType.StartArray)
            {
                var parts = JsonSerializer.Deserialize<List<ContentPart>>(ref reader, options);
                return new MessageContent(parts);
            }

            throw new JsonException("Invalid JSON for MessageContent");
        }

        public override void Write(Utf8JsonWriter writer, MessageContent value, JsonSerializerOptions options)
        {
            if (value.isString)
            {
                writer.WriteStringValue(value.content);
            }
            else if (value.isContentParts)
            {
                JsonSerializer.Serialize(writer, value.contentParts, options);
            }
            else
            {
                throw new JsonException("Invalid MessageContent state");
            }
        }
    }

    public class SystemMessage : Message
    {
        public SystemMessage() { }
        public SystemMessage(string message)
        {
            content = new MessageContent(message);
        }

        public override string role { get => "system"; }
    }

    public class UserMessage : Message
    {
        public UserMessage() { }
        public UserMessage(string message) { content = new MessageContent(message); }

        public override string role { get => "user"; }
    }

    public class AssistantMessage : Message
    {
        public override string role { get => "assistant"; }
    }

    public class VisionMessage : Message
    {
        public VisionMessage() { }
        public VisionMessage(List<ContentPart> parts)
        {
            content = new MessageContent(parts);
        }

        public override string role { get => "user"; }
    }

    public class MessageContent
    {
        public MessageContent(string text)
        {
            content = text;
        }

        public MessageContent(List<ContentPart> parts)
        {
            content = string.Empty;
            contentParts = parts;
        }

        public string content { get; set; }

        public List<ContentPart> contentParts { get; set; }

        public bool isString => !string.IsNullOrEmpty(content);

        public bool isContentParts => contentParts != null && contentParts.Count > 0;
    }

    public class ContentPart
    {
        public string type { get; set; }
        public string text { get; set; }
        public ImageUrlObject image_url { get; set; }

        public InputAudioObject input_audio { get; set; }
    }

    public class ImageUrlObject
    {
        public string url { get; set; }
        public string detail { get; set; }
    }

    public class InputAudioObject
    {
        public string data { get; set; }
        public string format { get; set; }
    }
}
