using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Query
{
    public interface ITaskStatusQuery
    {
        Task<List<Domain.Entities.TaskStatus>> GetListTaskStatus();
        Task<Domain.Entities.TaskStatus> GetTaskStatusById(int id);
    }
}
