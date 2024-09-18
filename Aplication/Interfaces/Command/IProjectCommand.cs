using Domain.Entities;

namespace Application.Interfaces.Command
{
    public interface IProjectCommand
    {
        System.Threading.Tasks.Task InsertProject(Project project);
        System.Threading.Tasks.Task AddProjectInteractions(Domain.Entities.Interaction interaction);
        System.Threading.Tasks.Task UpdateProjectTasks(Domain.Entities.Task task);
        System.Threading.Tasks.Task UpdateProject(Project project);
    }
}