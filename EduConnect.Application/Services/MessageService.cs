using AutoMapper;
using EduConnect.Application.Commons;
using EduConnect.Application.Commons.Dtos;
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
    public class MessageService(
                 IMessageRepository messageRepo,
            IMapper mapper
        ) : IMessageService
    {

        public Task<BaseResponse<object>> CreateMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<object>> CreateRangeMessages(List<Message> messages, Guid userId)
        {

            if (messages == null)
            {
                return BaseResponse<object>.Fail("Message cannot be null.");
            }

            if (messages.Count == 0)
            {
                return BaseResponse<object>.Fail("Message list cannot be empty.");
            }

            foreach (var message in messages)
            {
                message.CreatedAt = DateTime.Now;
                await messageRepo.AddAsync(message);
            }


            var result = await messageRepo.SaveChangesAsync();
            if (!result)
            {
                return BaseResponse<object>.Fail("Failed to create message.");
            }
            return BaseResponse<object>.Ok(messages);
        }

        public Task<BaseResponse<object>> DeleteMessage(Guid messageId)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IEnumerable<Message>>> GetAllMessagesByConversationId(Guid conversationId)
        {
            var messages = await messageRepo.GetAllMessagesByConversationIdAsync(conversationId);
            return BaseResponse<IEnumerable<Message>>.Ok(messages);
        }

        public Task<BaseResponse<Message>> GetMessageById(Guid messageId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<object>> UpdateMessage(Message messages)
        {
            throw new NotImplementedException();
        }
    }
}
