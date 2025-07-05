using AutoMapper;
using EduConnect.Application.Commons;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Domain.Entities;
using Microsoft.VisualBasic;
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

        public async Task<BaseResponse<object>> UpdateConversation(Guid conversationId, List<Message> newMessages, Guid userId)
        {
            var existingConversation = await conversationRepo.GetConversationByIdAsync(conversationId);

            if (existingConversation == null)
            {
                return BaseResponse<object>.Fail("Conversation not found.");
            }

            //if (existingConversation.ParentId == null || existingConversation.ParentId == Guid.Empty)
            //{
            //    existingConversation.ParentId = userId;
            //}

            //// Fix: Add each message individually to the Messages collection
            //foreach (var message in newMessages)
            //{
            //    existingConversation.Messages.Add(message);
            //}

            //conversationRepo.Update(existingConversation);



            try
            {
                var result = await conversationRepo.SaveChangesAsync();
                if (result == true)
                {
                    return BaseResponse<object>.Ok(existingConversation);
                }
                else
                {
                    return BaseResponse<object>.Fail("Failed to update conversation.");
                }
            }
            catch (Exception ex)
            {
                return BaseResponse<object>.Fail("Failed to update conversation.");
            }
        }
    }
}
