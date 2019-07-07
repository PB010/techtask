using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTask.Application.Comments.Mapping;
using TechTask.Application.Interfaces;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Task;

namespace TechTask.Infrastructure.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;
        private readonly IDbLogService _dbLogService;

        public CommentService(AppDbContext context, IDbLogService dbLogService)
        {
            _context = context;
            _dbLogService = dbLogService;
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
            await _context.SaveChangesAsync();
            
            return await _dbLogService.LogOnCreationOfEntity(comment);
        }

        public async Task<int> RemoveCommentAsync(int commentId)
        {
            var commentToRemove = await _context.Comments
                .SingleOrDefaultAsync(c => c.Id == commentId);
            _dbLogService.LogOnEntityDelete(commentToRemove);

            _context.Comments.Remove(commentToRemove);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateComment(Comment comment, CommentForUpdateDto dto)
        {
            comment.Description = dto.Description ??
                comment.Description;
            _dbLogService.LogOnUpdateOfAnEntity(comment);

            return await _context.SaveChangesAsync();
        }
    }
}
