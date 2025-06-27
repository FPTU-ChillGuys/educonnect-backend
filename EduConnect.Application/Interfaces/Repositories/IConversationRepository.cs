using EduConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Application.Interfaces.Repositories
{
    public interface IConversationRepository : IGenericRepository<Conversation>
    {
        Task<IEnumerable<Conversation>> GetAllConversationsByUserIdAsync(Guid userId, bool asNoTracking = false);
        Task<Conversation?> GetConversationByIdAsync(Guid conversationId, bool asNoTracking = false);
        //Task<bool> IsUserInConversationAsync(Guid userId, Guid conversationId);
        //Task AddMessageToConversationAsync(Guid conversationId, Message message);
        //Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(Guid conversationId, int skip = 0, int take = 50, bool asNoTracking = false);
    }
}
