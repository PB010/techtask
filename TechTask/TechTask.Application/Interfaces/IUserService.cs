using System;
using System.Threading.Tasks;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetSingleUserAsync(Guid id);
        void AddUser(User user);
        Task<int> SaveChangesAsync();
    }
}