using EduConnect.ChatbotAPI.Utils;
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

        //public async Task<string> SummaryChatbotHistoryAsync(Guid conversationId, CancellationToken ct = default)
        //{

        //}


        public async IAsyncEnumerable<string> ChatbotResponseAsync(string userPrompt, Guid conversationId, [EnumeratorCancellation] CancellationToken ct = default)
        {

            ////Lay lich su cuoc hoi thoai tu khoi tao
            //Conversation? conversation = await _chatbotStorage.GetConversation(conversationId);

            //if (conversation == null)
            //{
            //    //Neu khong co lich su, khoi tao mot cuoc hoi thoai moi
            //    conversation = new Conversation
            //    {
            //        ConversationId = conversationId,
            //        Messages = new List<Message>(),
            //        CreatedAt = DateTime.UtcNow,
            //        UpdatedAt = DateTime.UtcNow
            //    };
            //}

            //chatHistory = await _chatbotStorage.GetChatHistory(conversationId);
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
                yield return ChatbotUtils.RemoveThinkTags(response);
            }

            ////Them phan hoi cua chatbot vao lich su
            //chatHistory.Add(new ChatMessageContent(AuthorRole.Assistant, response));

        }

        public async Task<string> ChatbotResponseNonStreaming(string userPrompt, Guid conversationId, CancellationToken ct = default)
        {
            IChatCompletionService chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
            //Them prompt nguoi dung vao chat history
            chatHistory.Add(new ChatMessageContent(AuthorRole.User, userPrompt));

            OllamaPromptExecutionSettings ollamaPromptExecutionSettings = new();
            var response = await chatCompletionService.GetChatMessageContentsAsync(chatHistory, ollamaPromptExecutionSettings, _kernel, ct);

            return ChatbotUtils.RemoveThinkTags(response.ToString()!) ?? string.Empty;
        }
    }
}
