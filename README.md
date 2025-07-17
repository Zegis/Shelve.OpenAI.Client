# Shelve.OpenAI.Client

This is a small, minimalistic REST API Client made for OpenAI API platform created long time ago, before OpenAI SDK for .NET was available as part of learning course named AI Devs 2. Then used as part of solutions for AI Devs 3: Agents course.

The purpose was to slowly decouple all OpenAI related code into small library package to reuse on other projects.

## Capabilities:
- Completions Endpoint

## Simple Usage:

secrets.json:
```json
  "openAi": {
    "apiKey": "Your API KEY GOES HERE",
    "complationsEndpoint": "https://api.openai.com/v1/chat/completions",
    "audioEndpoint": "https://api.openai.com/v1/audio/",
    "imageEndpoint": "https://api.openai.com/v1/images/generations",
    "embeddingsEndpoint": "https://api.openai.com/v1/embeddings",
    "finetunedModelName": "NOT USED"
  }
```

program.cs:
```csharp
builder.Services.AddScoped<ICompletionsClient>(_ =>
    new CompletionsClient(new OpenAiConfiguration(builder.Configuration)));
```