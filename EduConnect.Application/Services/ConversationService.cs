using EduConnect.Application.Commons;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Application.Services
{
    public class ConversationService(
            IConversationRepository conversationRepo
        ) : IConversationService
    {
        public async Task<BaseResponse<IEnumerable<Conversation>>> GetAllConversationsByUserId(Guid userId)
        {
            var conversations = await conversationRepo.GetAllConversationsByUserIdAsync(userId);
            if (conversations == null || !conversations.Any())
            {
                return BaseResponse<IEnumerable<Conversation>>.Fail("No conversations found for this user.");
            }

            return BaseResponse<IEnumerable<Conversation>>.Ok(conversations);
        }

        public async Task<BaseResponse<Conversation>> GetConversationById(Guid conversationId)
        {
            var conversation = await conversationRepo.GetConversationByIdAsync(conversationId);
            if (conversation == null)
            {
                return BaseResponse<Conversation>.Fail("Conversation not found.");
            }
            return BaseResponse<Conversation>.Ok(conversation);
        }
    }
}
