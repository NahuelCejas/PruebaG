using Infrastructure.Persistence;
using Application.Interfaces.Query;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class TaskStatusQuery : ITaskStatusQuery
    {
        private readonly AppDbContext _context;

        public TaskStatusQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.TaskStatus>> GetListTaskStatus()
        {
            var taskStatus = await _context.TaskStatuses.ToListAsync(); 

            return taskStatus;
        }

        public async Task<Domain.Entities.TaskStatus> GetTaskStatusById(int id)
        {
            var taskStatus = await _context.TaskStatuses.FirstOrDefaultAsync(c => c.Id == id);

            return taskStatus;
        }
    }
}