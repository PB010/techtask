using System.Threading.Tasks;
using TechTask.Persistence.Models.Task;

namespace TechTask.Application.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> GetCommentAsync(int commentId);
        Task<int> AddNewCommentAsync(Comment comment);
    }
}