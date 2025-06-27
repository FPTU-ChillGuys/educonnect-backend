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
            IGenericRepository<Conversation> conversationRepo
        ) : IConversationService
    {
        public Task<BaseResponse<Conversation>> GetAllConversationsByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
