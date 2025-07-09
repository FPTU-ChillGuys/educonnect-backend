using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Domain.Entities;
using EduConnect.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Infrastructure.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(EduConnectDbContext context) : base(context)
        {
        }

        public async Task<Message?> GetMessageByIdAsync(Guid messageId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Message>> GetAllMessagesByConversationIdAsync(Guid conversationId)
        {
            var messageQuery = await _context.Messages
                .Where(m => m.ConversationId == conversationId)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
            return messageQuery;
        }

        public async Task<string> GetFirstMessageAsTitleByConversationIdAsync(Guid conversationId)
        {
            var firstMessage = await _context.Messages
                .Where(m => m.ConversationId == conversationId)
                .OrderBy(m => m.CreatedAt)
                .Select(m => m.Content)
                .FirstOrDefaultAsync();
            return firstMessage == null ? string.Empty : firstMessage?.Substring(0, Math.Min(firstMessage.Length, 10)) + "...";
        }
    }
}
