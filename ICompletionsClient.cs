using Shelve.OpenAi.Client.models;

namespace Shelve.OpenAi.Client
{
    public interface ICompletionsClient
    {
        Task<string> PostMessages(List<Message> message, string model = "gpt-4o-mini", float temperature = 0, bool printCompletions = true);
        Task<Message> Chat(List<Message> message, string model = "gpt-4o-mini", float temperature = 0);
        Task PostMessage(string message, string model = "gpt-4o-mini");
        Task<string> DescribeImagesFromDisc(string textContent, List<string> fileName, string model = "gpt-4o-mini", bool printRequest = true);
    }
}