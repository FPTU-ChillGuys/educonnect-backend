using EduConnect.Application.Interfaces.Services;
using EduConnect.ChatbotAPI.Services.Chatbot;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Enums;
using Microsoft.AspNetCore.SignalR;

namespace EduConnect.ChatbotAPI.Hubs
{
    public class ChatbotHub(
        ChatbotHelper chatbotHelper,
        ChatbotStorage chatbotStorage,
        IConversationService conversationService
        ) : Hub
    {

        public async Task SendMessageAsync(Guid conversationId, string message)
        {
            var conversation = await chatbotStorage.GetConversation(conversationId);

            Message newMessage = null;
            Message responseMessage = null;

            if (conversation != null)
            {
                newMessage = new Message
                {
                    Content = message,
                    ConversationId = conversation.ConversationId,
                    Role = MessageRole.User.ToString(),
                    CreatedAt = DateTime.UtcNow
                };
                conversation.Messages.Add(newMessage);

                await chatbotStorage.SaveConversationToCaching(conversationId, newMessage);
                await conversationService.UpdateConversation(conversation);

                await foreach (var res in chatbotHelper.ChatbotResponseAsync(message, conversationId))
                {
                    if (responseMessage == null)
                    {
                        responseMessage = new Message
                        {
                            Content = res,
                            ConversationId = conversation.ConversationId,
                            Role = MessageRole.Assistant.ToString(),
                            CreatedAt = DateTime.UtcNow
                        };
                    }
                    else
                    {
                        responseMessage.Content += res;
                    }
                    await Clients.Caller.SendAsync("ReceiveMessage", responseMessage.Content);
                }

                await chatbotStorage.SaveConversationToCaching(conversationId, responseMessage ?? new Message());
                await conversationService.UpdateConversation(conversation);
            }
        }
    }
}
