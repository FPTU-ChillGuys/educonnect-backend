using EduConnect.Application.Commons;
using EduConnect.Application.Commons.Dtos;
using EduConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Application.Interfaces.Services
{
    public interface IMessageService
    {
        Task<BaseResponse<IEnumerable<Message>>> GetAllMessagesByConversationId(Guid conversationId);

        Task<BaseResponse<Message>> GetMessageById(Guid messageId);

        Task<BaseResponse<object>> CreateMessage( Message message);

        Task<BaseResponse<object>> CreateRangeMessages(List<Message> messages, Guid userId);   

        Task<BaseResponse<object>> UpdateMessage(Message messages);

        Task<BaseResponse<object>> DeleteMessage(Guid messageId);


    }
}
