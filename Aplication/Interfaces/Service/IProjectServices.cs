using Application.Request;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IProjectServices
    {
        Task<ProjectDetails> GetProjectById(Guid id);
        Task<ProjectDetails> CreateProject(ProjectRequest project);
        Task<Interactions> AddInteraction(Guid projectId, InteractionsRequest interaction);
        Task<Tasks> AddTask(Guid projectId, TasksRequest task);
        Task<Tasks> UpdateTask(Guid taskId, TasksRequest task);
        Task<List<Application.Response.Project>> GetProjects(string? name, int? campaign, int? client, int? offset, int? size);
    }
}