using EduConnect.ChatbotAPI.Utils;
using EduConnect.Domain.Entities;
using Hangfire.Logging;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Google;
using Microsoft.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Runtime.CompilerServices;

namespace EduConnect.ChatbotAPI.Services.Chatbot
{
    public class ChatbotHelper
    {
        ChatHistory chatHistory = new ChatHistory();

        private readonly Kernel _kernel;
        private readonly HttpClient _httpClient;
        private readonly ChatbotStorage _chatbotStorage;
        private readonly ILogger<ChatbotHelper> _logger;


        public ChatbotHelper(Kernel kernel, HttpClient httpClient, ChatbotStorage chatbotStorage)
        {
            _kernel = kernel;
            _httpClient = httpClient;
            _chatbotStorage = chatbotStorage;
            _logger = kernel.GetRequiredService<ILogger<ChatbotHelper>>();


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

            //Them system prompt
            chatHistory.AddSystemMessage("If you can't find the answer, try using function calls to locate the information");
            chatHistory.AddSystemMessage("If data have IDs, for example classId, studentId, etc, don't show them");

            //Them prompt nguoi dung vao chat history
            chatHistory.Add(new ChatMessageContent(AuthorRole.User, userPrompt));

            #pragma warning disable SKEXP0070
            //OllamaPromptExecutionSettings ollamaPromptExecutionSettings = new()
            //{
            //    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            //};

            GeminiPromptExecutionSettings geminiPromptExecutionSettings = new()
            {
                //FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                ToolCallBehavior = GeminiToolCallBehavior.AutoInvokeKernelFunctions
            };

            //IAsyncEnumerable<StreamingChatMessageContent> streamingEnumerable = chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory, geminiPromptExecutionSettings, _kernel, ct);
            //await using var streamingEnumerator = streamingEnumerable.GetAsyncEnumerator(ct);

            //for (var more = true; more;)
            //{
            //    // Catch exceptions only on executing/resuming the iterator function
            //    try
            //    {
            //        more = await streamingEnumerator.MoveNextAsync();
            //    }
            //    catch (Exception ex)
            //    {
            //        _logger.LogError(ex, "Error while streaming chat message contents.");
            //        throw;
            //    }
            //    response += streamingEnumerator.Current?.Content ?? string.Empty;
            //    yield return ChatbotUtils.RemoveThinkTags(response);
            //}


            await foreach (var item in chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory, geminiPromptExecutionSettings, _kernel, ct))
            {
                response += item;
                yield return response;
            }

            ////Them phan hoi cua chatbot vao lich su
            //chatHistory.Add(new ChatMessageContent(AuthorRole.Assistant, response));

        }

        public async Task<string?> ChatbotResponseNonStreaming(string userPrompt, CancellationToken ct = default)
        {
            IChatCompletionService chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
            //Them prompt nguoi dung vao chat history
            chatHistory.Add(new ChatMessageContent(AuthorRole.User, userPrompt));


            GeminiPromptExecutionSettings geminiPromptExecutionSettings = new()
            {
                //FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                ToolCallBehavior = GeminiToolCallBehavior.AutoInvokeKernelFunctions
            };

            string response = string.Empty;

            await foreach (var item in chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory, geminiPromptExecutionSettings, _kernel, ct))
            {
                response += item;
            }

            return response != null ? response.ToString() : string.Empty;
        }
    }
}
