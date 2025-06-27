using EduConnect.Domain.Entities;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;
using System.Runtime.CompilerServices;

namespace EduConnect.ChatbotAPI.Services.Chatbot
{
    public class ChatbotHelper
    {
        ChatHistory chatHistory = new ChatHistory();

        private readonly Kernel _kernel;
        private readonly HttpClient _httpClient;
        private readonly ChatbotStorage _chatbotStorage;


        public ChatbotHelper(Kernel kernel, HttpClient httpClient, ChatbotStorage chatbotStorage)
        {
            _kernel = kernel;
            _httpClient = httpClient;
            _chatbotStorage = chatbotStorage;

        }


        public async IAsyncEnumerable<string> ChatbotResponseAsync(string userPrompt, Guid conversationId, [EnumeratorCancellation] CancellationToken ct = default)
        {

            //Lay lich su cuoc hoi thoai tu khoi tao
            Conversation? conversation = await _chatbotStorage.GetConversation(conversationId);
            
            

                IChatCompletionService chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
            string response = string.Empty;

            //Them prompt nguoi dung vao chat history
            chatHistory.Add(new ChatMessageContent(AuthorRole.User, userPrompt));

            #pragma warning disable SKEXP0070
            OllamaPromptExecutionSettings ollamaPromptExecutionSettings = new()
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            };

            await foreach (var item in chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory, ollamaPromptExecutionSettings, _kernel, ct))
            {
                response += item;
                yield return response;
            }

        }
    }
}
