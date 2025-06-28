using AutoMapper;
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
            IConversationRepository conversationRepo,
            IMapper mapper
        ) : IConversationService
    {
        public async Task<BaseResponse<object>> CreateConversation(Conversation conversation)
        {
            await conversationRepo.AddAsync(conversation);
            var result = await conversationRepo.SaveChangesAsync();

            if (!result)
            {
                return BaseResponse<object>.Fail("Failed to create conversation.");
            }
            return BaseResponse<object>.Ok(new { conversationId = conversation.ConversationId });

        }

        public Task<BaseResponse<object>> DeleteConversation(Guid conversationId)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IEnumerable<Conversation>>> GetAllConversationsByUserId(Guid userId)
        {
            var conversations = await conversationRepo.GetAllConversationsByUserIdAsync(userId);
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

        public async Task<BaseResponse<object>> UpdateConversation(Conversation conversation)
        {
            var existingConversation = await conversationRepo.GetByIdAsync(conversation.ConversationId);

            if (existingConversation == null)
            {
                return BaseResponse<object>.Fail("Conversation not found.");
            }

            mapper.Map(conversation, existingConversation);
            conversationRepo.Update(existingConversation);

            return await conversationRepo.SaveChangesAsync()
                ? BaseResponse<object>.Ok(existingConversation)
                : BaseResponse<object>.Fail("Failed to update conversation.");
        }
    }
}
