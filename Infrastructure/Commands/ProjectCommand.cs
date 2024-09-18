using Infrastructure.Persistence;
using Application.Interfaces.Command;
using Domain.Entities;
using System;

namespace Infrastructure.Command
{
    public class ProjectCommand : IProjectCommand
    {
        private readonly AppDbContext _context;

        public ProjectCommand(AppDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task InsertProject(Project project)
        {
            _context.Add(project);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateProject(Project project)
        {
            _context.Update(project);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task AddProjectInteractions(Interaction interaction)
        {
            _context.Add(interaction);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateProjectTasks(Domain.Entities.Task task)
        {
            // Verifica si la tarea ya existe en la base de datos
            var existingTask = await _context.Tasks.FindAsync(task.TaskID);
            if (existingTask == null)
            {
                throw new Exception("Task not found for update.");
            }

            // Actualiza los valores de la tarea existente
            _context.Entry(existingTask).CurrentValues.SetValues(task);

            await _context.SaveChangesAsync();
        }
    }
}