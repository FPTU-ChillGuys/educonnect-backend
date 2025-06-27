using EduConnect.ChatbotAPI.Services.Chatbot;
using EduConnect.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace EduConnect.ChatbotAPI.Hubs
{
    public class ChatbotHub(
        ChatbotHelper chatbotHelper
        ) : Hub
    {
        Message newMessage = null!;


    }
}
