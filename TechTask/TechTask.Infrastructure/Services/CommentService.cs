using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Task;

namespace TechTask.Infrastructure.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetCommentAsync(int commentId)
        {
            return await _context.Comments.SingleOrDefaultAsync(c => c.Id == commentId);
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync(int taskId)
        {
            return await _context.Comments.Where(c => c.TasksId == taskId)
                .ToListAsync();
        }

        public async Task<int> AddNewCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveCommentAsync(int commentId)
        {
            var commentToRemove = await _context.Comments
                .SingleOrDefaultAsync(c => c.Id == commentId);

            _context.Comments.Remove(commentToRemove);
            return await _context.SaveChangesAsync();
        }
    }
}
