using Application.Interfaces.Query;
using Application.Interfaces.Service;
using Application.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class TaskStatusServices : ITaskStatusServices
    {
        private readonly ITaskStatusQuery _taskStatusQuery;

        public TaskStatusServices(ITaskStatusQuery query)
        {
            _taskStatusQuery = query;
        }

        public async Task<List<GenericResponse>> GetAll()
        {
            var taskStatus = await _taskStatusQuery.GetListTaskStatus();
            var result = taskStatus.Select(ts => new GenericResponse
            {
                Id = ts.Id,
                Name = ts.Name
            }).ToList();
            return result;
        }
    }
}