using EduConnect.Application.Interfaces.Services;
using EduConnect.ChatbotAPI.Services.Chatbot;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace EduConnect.ChatbotAPI.Hubs
{
    public class ChatbotHub(
        ChatbotHelper chatbotHelper,
        ChatbotStorage chatbotStorage,
        IConversationService conversationService,
        IMessageService messageService,
        ILogger<ChatbotHub> logger
        ) : Hub
    {

        public async Task SendMessageAsync(string userId, string conversationId, string message)
        {
            //var conversation = await chatbotStorage.GetConversation(Guid.Parse(conversationId));
            //var result = await conversationService.GetConversationById(Guid.Parse(conversationId));
            List<Message> messages = new();
            
            //if (result.Success)
            //{
            //    messages = result.Data!.Messages.ToList();
            //}

            Message newMessage = null;
            Message responseMessage = null;

            if (messages != null)
            {
                newMessage = new Message
                {
                    Content = message,
                    ConversationId = Guid.Parse(conversationId),
                    Role = MessageRole.User.ToString(),
                    CreatedAt = DateTime.UtcNow
                };
                messages.Add(newMessage);

                //await chatbotStorage.SaveConversationToCaching(Guid.Parse(conversationId), newMessage);
                //await conversationService.UpdateConversation(conversation);

                await foreach (var res in chatbotHelper.ChatbotResponseAsync(message, Guid.Parse(conversationId)))
                {
                    if (responseMessage == null)
                    {
                        responseMessage = new Message
                        {
                            MessageId = Guid.NewGuid(),
                            Content = res, 
                            ConversationId = Guid.Parse(conversationId),
                            Role = MessageRole.Assistant.ToString(),
                            CreatedAt = DateTime.UtcNow
                        };
                    }
                    else
                    {
                        responseMessage.Content = res;
                    }
                    logger.LogInformation("Code in here");
                    logger.LogInformation("Code in here");
                    logger.LogInformation("Code in here");
                    logger.LogInformation("Code in here");
                    logger.LogInformation("Code in here");
                    logger.LogInformation("Code in here");
                    logger.LogInformation("Code in here");
                    await Clients.Caller.SendAsync("ReceiveMessage", responseMessage.MessageId, responseMessage.Content);
                }

                //await chatbotStorage.SaveConversationToCaching(Guid.Parse(conversationId), responseMessage ?? new Message());
                messages.Add(responseMessage!);
                await messageService.CreateRangeMessages(messages);
            }
        }
    }
}
