using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Runtime.CompilerServices;

namespace EduConnect.ChatbotAPI.Services.Chatbot
{
    public class ChatbotHelper
    {
        ChatHistory chatHistory = new ChatHistory();

        private readonly Kernel _kernel;
        private readonly HttpClient _httpClient;


        public ChatbotHelper(Kernel kernel, HttpClient httpClient)
        {
            _kernel = kernel;
            _httpClient = httpClient;

            chatHistory.AddAssistantMessage("You are a good asistant!");
            chatHistory.AddDeveloperMessage("If user say hello, say hello");
        }


        public async IAsyncEnumerable<string> ChatbotResponseAsync(string userPrompt, object conversationId, [EnumeratorCancellation] CancellationToken ct = default)
        {
            IChatCompletionService chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
            string response = string.Empty;

            //Them prompt nguoi dung vao chat history
            chatHistory.Add(new ChatMessageContent(AuthorRole.User, userPrompt));

            yield return "Processing your request...";

        }


    }
}
