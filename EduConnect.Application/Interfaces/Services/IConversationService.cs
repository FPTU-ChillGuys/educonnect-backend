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
        Task<BaseResponse<Conversation>> GetAllConversationsByUserId(Guid userId);

    }
}
