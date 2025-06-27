using EduConnect.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace EduConnect.ChatbotAPI.Services.Chatbot
{
    public class ChatbotStorage(
            IDistributedCache cache
        )
    {

        public async Task SaveConversation(Guid converstationId, Message message)
        {
            var key = converstationId.ToString();

            var conversationJson = await cache.GetStringAsync(key);

            var conversation = string.IsNullOrEmpty(conversationJson) ? 
                new Conversation { ConversationId = converstationId } : 
                JsonSerializer.Deserialize<Conversation>(conversationJson) 
                ?? new Conversation { ConversationId = converstationId };

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
                return null;
            }
            return JsonSerializer.Deserialize<Conversation>(conversationJson);
        }


    }
}
