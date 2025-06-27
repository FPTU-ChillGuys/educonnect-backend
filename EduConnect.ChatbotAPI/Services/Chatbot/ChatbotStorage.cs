using EduConnect.Application.Interfaces.Services;
using EduConnect.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace EduConnect.ChatbotAPI.Services.Chatbot
{
    public class ChatbotStorage(
            IDistributedCache cache,
            IConversationService conversationService
        )
    {

        public async Task SaveConversation(Guid converstationId, Message message)
        {
            var key = converstationId.ToString();

            var conversationJson = await cache.GetStringAsync(key);

            var conversation = await GetConversation(converstationId) ?? new Conversation
            {
                ConversationId = converstationId,
                Messages = new List<Message>(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            conversation.Messages.Add(message);
            converstationId = conversation.ConversationId;
            conversation.UpdatedAt = DateTime.UtcNow;
            
            var updatedConversationJson = JsonSerializer.Serialize(conversation);
            await cache.SetStringAsync(key, updatedConversationJson, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) // Set expiration as needed
            });
        }

        public async Task<Conversation?> GetConversation(Guid conversationId)
        {
            var key = conversationId.ToString();
            var conversationJson = await cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(conversationJson))
            { 
                var conversation = await conversationService.GetConversationById(conversationId);
                if (conversation.Success && conversation.Data != null)
                {
                    return conversation.Data; 
                } else
                {
                    return null; // or throw an exception based on your error handling strategy
                }
            }
            return JsonSerializer.Deserialize<Conversation>(conversationJson);
        }


    }
}
