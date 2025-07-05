using EduConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Application.Interfaces.Repositories
{
    public interface IMessageRepository : IGenericRepository<Message>
    {

        //Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(Guid conversationId, int skip = 0, int take = 50, bool asNoTracking = false);
        Task<IEnumerable<Message>> GetAllMessagesByConversationIdAsync(Guid conversationId);
        //Task<int> GetMessageCountByConversationIdAsync(Guid conversationId);
        Task<Message?> GetMessageByIdAsync(Guid messageId);
        //Task<IEnumerable<Message>> GetMessagesByUserIdAsync(Guid userId, int skip = 0, int take = 50, bool asNoTracking = false);
    }
}
