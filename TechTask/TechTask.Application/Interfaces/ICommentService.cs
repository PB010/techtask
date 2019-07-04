using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Persistence.Models.Task;

namespace TechTask.Application.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> GetCommentAsync(int commentId);
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<int> AddNewCommentAsync(Comment comment);
    }
}