using System.Threading.Tasks;

namespace TechTask.Application.Interfaces
{
    public interface IDbLogService
    {
        Task<int> LogOnCreationOfEntity(object entity);
        void LogOnUpdateOfAnEntity(object oldEntity);
        void LogOnEntityDelete(object oldEntity);
    }
}