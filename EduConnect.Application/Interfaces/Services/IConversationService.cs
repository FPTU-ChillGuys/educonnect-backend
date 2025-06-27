using EduConnect.Application.Commons;
using EduConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Application.Interfaces.Services
{
    public interface IConversationService
    {
        Task<BaseResponse<IEnumerable<Conversation>>> GetAllConversationsByUserId(Guid userId);

        Task<BaseResponse<Conversation>> GetConversationById(Guid conversationId);

        Task<BaseResponse<object>> CreateConversation(Conversation conversation);

        Task<BaseResponse<object>> UpdateConversation(Conversation conversation);

        Task<BaseResponse<object>> DeleteConversation(Guid conversationId);

    }
}
