using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Domain.Entities;
using EduConnect.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;


namespace EduConnect.Infrastructure.Repositories
{
    public class ConversationRepository : GenericRepository<Conversation>, IConversationRepository
    {
        public ConversationRepository(EduConnectDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Conversation>> GetAllConversationsByUserIdAsync(Guid userId)
        {
            var conversations = await _context.Conversations
                .Where(c => c.ParentId == userId)
                .Include(c => c.Parent)
                .Include(c => c.Messages)
                .ToListAsync();
            return conversations;
        }

        public async Task<Conversation?> GetConversationByIdAsync(Guid conversationId)
        {
            var conversationQuery = await _context.Conversations
                .Where(c => c.ConversationId == conversationId)
                .Include(c => c.Parent)
                .Include(c => c.Messages)
                .FirstOrDefaultAsync();
            return conversationQuery;
        }
    }
}
