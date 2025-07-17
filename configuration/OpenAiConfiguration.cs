using Microsoft.Extensions.Configuration;

namespace Shelve.OpenAi.Client.configuration
{
    public class OpenAiConfiguration
    {
        private readonly IConfigurationSection _openAiConfiguration;

        public OpenAiConfiguration(IConfigurationRoot configuration)
        {
            _openAiConfiguration = configuration.GetRequiredSection("openAi");
        }

        public string ApiKey => _openAiConfiguration["apiKey"];
        public string CompletionsEndpoint => _openAiConfiguration["complationsEndpoint"];
        public string AudioEndpoint => _openAiConfiguration["audioEndpoint"];
        public string ImageEndpoint => _openAiConfiguration["imageEndpoint"];
        public string EmbeddingsEndpoint => _openAiConfiguration["embeddingsEndpoint"];
        public string FinetunedModelName => _openAiConfiguration["finetunedModelName"];
    }
}
