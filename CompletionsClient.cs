using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Shelve.OpenAi.Client.configuration;
using Shelve.OpenAi.Client.models;
using Shelve.OpenAi.Client.requests;
using Shelve.OpenAi.Client.responses;

namespace Shelve.OpenAi.Client
{
    public class CompletionsClient: ICompletionsClient
    {
        private readonly string apiKey;
        private readonly string baseEndpoint;
        public CompletionsClient(OpenAiConfiguration config)
        {
            apiKey = config.ApiKey;
            baseEndpoint = config.CompletionsEndpoint;
        }

        public async Task<string> PostMessages(List<Message> message, string model = "gpt-4o-mini", float temperature = 0, bool printCompletions = true)
        {
            if (message.Count == 0)
                return string.Empty;

            var requestModel = new CompletionsRequest
            {
                model = model,
                messages = message,
                temperature = temperature
            };

            var responseObject = await PostCompletions(requestModel, printCompletions);

            return responseObject.choices[0].message.content.content;
        }

        public async Task<Message> Chat(List<Message> message, string model = "gpt-4o-mini", float temperature = 0)
        {
            if (message.Count == 0)
                return null;
            var requestModel = new CompletionsRequest
            {
                model = model,
                messages = message,
                temperature = temperature
            };
            var responseObject = await PostCompletions(requestModel, false);
            return responseObject.choices[0].message;
        }

        public async Task PostMessage(string message, string model = "gpt-4o-mini")
        {
            var requestModel = new CompletionsRequest
            {
                model = model,
                messages = new List<Message>
                {
                    new Message {role = "user", content = new MessageContent(message)}
                }
            };

            var responseObject = await PostCompletions(requestModel);

            Console.WriteLine(responseObject.choices[0].message.content);
        }

        public async Task<string> DescribeImagesFromDisc(string textContent, List<string> fileName, string model = "gpt-4o-mini", bool printRequest = true)
        {
            var messages = new List<Message>();

            var Images = new List<ContentPart>()
            {
                new ContentPart {type = "text", text = textContent}
            };

            foreach (var base64 in fileName)
            {
                Images.Add(new ContentPart { type = "image_url", image_url = new ImageUrlObject() { url = base64 } });
            }

            messages.Add(new VisionMessage(Images));

            var requestModel = new CompletionsRequest
            {
                model = model,
                messages = messages
            };

            var responseObject = await PostCompletions(requestModel, PrintRequest: printRequest);

            Console.WriteLine(responseObject.choices[0].message.content);

            return responseObject.choices[0].message.content.content;
        }

        private string ConvertFileToBase64(string fileName)
        {
            return Convert.ToBase64String(File.ReadAllBytes(fileName));
        }

        private async Task<CompletionResponse> PostCompletions(CompletionsRequest requestModel, bool PrintRequest = true)
        {
            JsonSerializerOptions options = new()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var request = JsonSerializer.Serialize(requestModel, options);
            var content = new StringContent(request);

            if (PrintRequest)
                Console.WriteLine(request);

            HttpRequestMessage Httpmessage = new HttpRequestMessage(HttpMethod.Post, baseEndpoint);

            Httpmessage.Content = content;
            Httpmessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            Httpmessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(5);
            var response = await client.SendAsync(Httpmessage);

            var responseObject = JsonSerializer.Deserialize<CompletionResponse>(response.Content.ReadAsStringAsync().Result);

            return responseObject;
        }
    }
}
