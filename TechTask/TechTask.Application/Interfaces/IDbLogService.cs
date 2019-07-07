using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Application.DbLogs.Models;
using TechTask.Persistence.Models.Logs;

namespace TechTask.Application.Interfaces
{
    public interface IDbLogService
    {
        Task<IEnumerable<UpdateLog>> GetAllLogs(DbLogQueryParameters query);
        Task<int> LogOnCreationOfEntity(object entity);
        void LogOnUpdateOfAnEntity(object oldEntity);
        void LogOnEntityDelete(object oldEntity);
    }
}