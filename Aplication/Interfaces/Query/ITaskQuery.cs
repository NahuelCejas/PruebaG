using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces.Query
{
    public interface ITaskQuery
    {
        Task<Domain.Entities.Task> GetTaskById(Guid id);
    }
}