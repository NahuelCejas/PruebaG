using Infrastructure.Persistence;
using Application.Interfaces.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class TaskQuery : ITaskQuery
    {
        private readonly AppDbContext _context;

        public TaskQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Task> GetTaskById(Guid id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(c => c.TaskID == id); 

            return task;
        }
    }
}