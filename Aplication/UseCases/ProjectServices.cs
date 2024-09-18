using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Interfaces.Service;
using Application.Request;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class ProjectServices : IProjectServices
    {
        private readonly IProjectQuery _projectQuery;
        private readonly IProjectCommand _projectCommand;
        private readonly IInteractionTypeQuery _interactionTypeQuery;
        private readonly IUserQuery _userQuery;
        private readonly ITaskStatusQuery _taskStatusQuery;
        private readonly IClientQuery _clientQuery;
        private readonly ICampaignTypeQuery _campaignTypeQuery;
        private readonly ITaskQuery _taskQuery;

        public ProjectServices(IProjectQuery query, IProjectCommand command, IInteractionTypeQuery interactionTypeQuery, IUserQuery userQuery,
                                ITaskStatusQuery taskStatusQuery, IClientQuery clientQuery, ICampaignTypeQuery campaignTypeQuery, ITaskQuery taskQuery)
        {
            _projectQuery = query;
            _projectCommand = command;
            _interactionTypeQuery = interactionTypeQuery;
            _userQuery = userQuery;
            _taskStatusQuery = taskStatusQuery;
            _clientQuery = clientQuery;
            _campaignTypeQuery = campaignTypeQuery;
            _taskQuery = taskQuery;
        }
        public async Task<Interactions> AddInteraction(Guid projectId, InteractionsRequest interactionRequest)
        {
            //Validacion de datos ingresados no nulos ni vacios
            ValidateInteractionRequest(interactionRequest);
            try
            {
                var project = await _projectQuery.GetProjectById(projectId);
                //Validacion de que proyecto existe
                await ValidateProjectExistsAsync(projectId);

                //Domain.Entities.Project project = await _projectQuery.GetProjectById(id);
                //if (project == null)
                //{
                //    throw new NotFoundException("Project not found");
                //}

                var interactionTypeNameById = _interactionTypeQuery.GetInteractionTypeById(interactionRequest.InteractionType).Result.Name;
                //Validacion de que tipo de interaccion existe
                if (interactionTypeNameById == null)
                {
                    throw new NotFoundException("Interaction type not found.");
                }

                var newInteraction = new Interaction
                {
                    InteractionID = Guid.NewGuid(),
                    Notes = interactionRequest.Notes,
                    Date = interactionRequest.Date,
                    ProjectID = projectId,
                    InteractionType = interactionRequest.InteractionType,
                };

                await _projectCommand.AddProjectInteractions(newInteraction);
                project.UpdateDate = DateTime.Now;
                await _projectCommand.UpdateProject(project);

                return new Interactions
                {
                    Id = newInteraction.InteractionID,
                    Notes = newInteraction.Notes,
                    Date = newInteraction.Date,
                    ProjectId = newInteraction.ProjectID,
                    InteractionType = new GenericResponse
                    {
                        Id = newInteraction.InteractionType,
                        Name = interactionTypeNameById,
                    }
                };
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<Tasks> AddTask(Guid projectId, TasksRequest task)
        {
            //Validacion de datos ingresados no nulos ni vacios
            ValidateTaskRequest(task);
            try
            {
                var project = await _projectQuery.GetProjectById(projectId);
                //Validacion de que proyecto existe
                await ValidateProjectExistsAsync(projectId);
                //Domain.Entities.Project project = await _projectQuery.GetProjectById(projectId);
                //if (project == null)
                //{
                //    throw new NotFoundException("Project not found");
                //}

                var newTask = new Domain.Entities.Task
                {
                    TaskID = Guid.NewGuid(),
                    Name = task.Name,
                    DueDate = task.DueDate,
                    ProjectID = projectId,
                    Status = task.Status,
                    AssignedTo = task.User,
                    CreateDate = DateTime.Now,
                };
                await _projectCommand.UpdateProjectTasks(newTask);
                project.UpdateDate = DateTime.Now;
                await _projectCommand.UpdateProject(project);

                var newTaskStatus = await _taskStatusQuery.GetTaskStatusById(task.Status);
                var newUser = await _userQuery.GetUserById(task.User);

                return new Tasks
                {
                    Id = newTask.TaskID,
                    Name = newTask.Name,
                    DueDate = newTask.DueDate,
                    ProjectId = newTask.ProjectID,
                    Status = new GenericResponse 
                    {
                        Id = newTaskStatus.Id,
                        Name = newTaskStatus.Name,
                    },
                    UserAssigned = new Users
                    {
                        UserID = newUser.UserID,
                        Name = newUser.Name,
                        Email = newUser.Email,
                    }
                };
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<ProjectDetails> CreateProject(ProjectRequest projectRequest)
        {
            //Validacion de datos ingresados no nulos ni vacios
            ValidateProjectRequest(projectRequest);
            
            // Verificacion de mismo nombre
            var existingProject = await _projectQuery.GetProjectByName(projectRequest.Name);
            if (existingProject != null)
            {
                throw new BadRequest("A project with the same name already exists.");
            }

            var project = new Domain.Entities.Project
            {
                ProjectName = projectRequest.Name,
                StartDate = projectRequest.Start,
                EndDate = projectRequest.End,
                ClientID = projectRequest.Client,
                CampaignType = projectRequest.CampaignType,
                CreateDate = DateTime.Now,
            };
            await _projectCommand.InsertProject(project);

            var client = await _clientQuery.GetClientById(projectRequest.Client);
            var campaignType = await _campaignTypeQuery.GetCampaignTypeById(projectRequest.CampaignType);

            return new ProjectDetails
            {
                Data = new Application.Response.Project
                {
                    Id = project.ProjectID,
                    Name = project.ProjectName,
                    Start = project.StartDate,
                    End = project.EndDate,
                    Client = new Clients
                    {
                        Id = client.ClientID,
                        Name = client.Name,
                        Email = client.Email,
                        Company = client.Company,
                        Phone = client.Phone,
                        Address = client.Address,
                    },
                    CampaignType = new GenericResponse
                    {
                        Id = campaignType.Id,
                        Name = campaignType.Name,
                    }
                },
                Interactions = new List<Interactions>(),
                Tasks = new List<Tasks>()
            };
        }

        public async Task<ProjectDetails> GetProjectById(Guid id)
        {
            try
            {
                var project = await _projectQuery.GetProjectById(id);
                //Validacion de que proyecto existe
                await ValidateProjectExistsAsync(id);
                
                //Domain.Entities.Project project = await _projectQuery.GetProjectById(id);
                //if (project == null)
                //{
                //    throw new NotFoundException("Project not found");
                //}

                var client = await _clientQuery.GetClientById(project.ClientID);
                var campaignType = await _campaignTypeQuery.GetCampaignTypeById(project.CampaignType); 

                var data = new Application.Response.Project
                {
                    Id = project.ProjectID,
                    Name = project.ProjectName,
                    Start = project.StartDate,
                    End = project.EndDate,
                    Client = new Clients
                    {
                        Id = client.ClientID,
                        Name = client.Name,
                        Email = client.Email,
                        Company = client.Company,
                        Phone = client.Phone,
                        Address = client.Address,
                    },
                    CampaignType = new GenericResponse
                    {
                        Id = campaignType.Id,
                        Name = campaignType.Name
                    }
                };

                var interactions = project.Interactions.Select(interaction => new Interactions
                {
                    Id = interaction.InteractionID,
                    Notes = interaction.Notes,
                    Date = interaction.Date,
                    ProjectId = interaction.ProjectID,
                    InteractionType = new GenericResponse
                    {
                        Id = interaction.InteractionTypes.Id,
                        Name = interaction.InteractionTypes.Name,
                    }
                }).ToList();

                var tasks = project.Tasks.Select(task => new Tasks
                {
                    Id = task.TaskID,
                    Name = task.Name,
                    DueDate = task.DueDate,
                    ProjectId = task.ProjectID,
                    Status = new GenericResponse
                    {
                        Id = task.TaskStatus.Id,
                        Name = task.TaskStatus.Name
                    },
                    UserAssigned = new Users
                    {
                        UserID = task.User.UserID,
                        Name = task.User.Name,
                        Email = task.User.Email
                    }
                }).ToList();

                return await System.Threading.Tasks.Task.FromResult(new ProjectDetails
                {
                    Data = data,
                    Interactions = interactions,
                    Tasks = tasks
                });
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<Tasks> UpdateTask(Guid taskId, TasksRequest taskRequest)
        {
            //Validacion de datos ingresados no nulos ni vacios
            ValidateTaskRequest(taskRequest);

            //Validacion de task existente
            var task = await _taskQuery.GetTaskById(taskId);
            if (task == null)
            {
                throw new NotFoundException("Task not found");
            }

            task.Name = taskRequest.Name;
            task.DueDate = taskRequest.DueDate;
            task.Status = taskRequest.Status;
            task.AssignedTo = taskRequest.User;
            task.UpdateDate = DateTime.Now;

            await _projectCommand.UpdateProjectTasks(task);

            var taskStatus = await _taskStatusQuery.GetTaskStatusById(taskRequest.Status);
            var user = await _userQuery.GetUserById(taskRequest.User);

            return new Tasks
            {
                Id = task.TaskID,
                Name = task.Name,
                DueDate = task.DueDate,
                ProjectId = task.ProjectID,
                Status = new GenericResponse
                {
                    Id = taskStatus.Id,
                    Name = taskStatus.Name,
                },
                UserAssigned = new Users
                {
                    UserID = user.UserID,
                    Name = user.Name,
                    Email = user.Email,
                }
            };
        }

        public async Task<List<Response.Project>> GetProjects(string? name, int? campaign, int? client, int? offset, int? size)
        {
            var projects = await _projectQuery.GetProjects(name, campaign, client, offset, size);
            var responseProjects = new List<Response.Project>();

            foreach (var project in projects)
            {
                var clientQuery = await _clientQuery.GetClientById(project.ClientID);
                var campaignType =  await _campaignTypeQuery.GetCampaignTypeById(project.CampaignType);

                var responseProject = new Response.Project
                {
                    Id = project.ProjectID,
                    Name = project.ProjectName,
                    Start = project.StartDate,
                    End = project.EndDate,
                    Client = new Clients
                    {
                        Id = clientQuery.ClientID,
                        Name = clientQuery.Name,
                        Email = clientQuery.Email,
                        Company = clientQuery.Company,
                        Phone = clientQuery.Phone,
                        Address = clientQuery.Address,
                    },
                    CampaignType = new GenericResponse
                    {
                        Id = campaignType.Id,
                        Name = campaignType.Name,
                    }
                };
                responseProjects.Add(responseProject);
            }
            return responseProjects;
        }

        // Métodos privados para validaciones
        private async System.Threading.Tasks.Task ValidateProjectExistsAsync(Guid projectId)
        {
            var project = await _projectQuery.GetProjectById(projectId);

            if (project == null)
            {
                throw new NotFoundException("Project not found");
            }
        }

        private void ValidateInteractionRequest(InteractionsRequest request)
        {
            if (request == null)
            {
                throw new BadRequest("Interaction request cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(request.Notes))
            {
                throw new BadRequest("Notes cannot be null or empty.");
            }

            if (request.Date == default)
            {
                throw new BadRequest("Interaction date is required and cannot be default value.");
            }

            if (request.InteractionType <= 0)
            {
                throw new BadRequest("Interaction type must be a positive integer.");
            }
        }

        private void ValidateTaskRequest(TasksRequest taskRequest)
        {
            if (string.IsNullOrWhiteSpace(taskRequest.Name))
            {
                throw new BadRequest("Task name can't be null, empty, or whitespace.");
            }

            if (taskRequest.DueDate < DateTime.Now) //DateTime nunca puede ser null
            {
                throw new BadRequest("Due date can't be null or in the past.");
            }

            if (taskRequest.User <= 0)
            {
                throw new BadRequest("User assigned to the task must be provided and be a valid integer.");
            }

            if (taskRequest.Status <= 0)
            {
                throw new BadRequest("Task status must be provided and be a valid integer.");
            }
        }

        private void ValidateProjectRequest(ProjectRequest projectRequest)
        {
            if (string.IsNullOrWhiteSpace(projectRequest.Name))
            {
                throw new BadRequest("Project name can't be null, empty, or whitespace.");
            }

            if (projectRequest.End < projectRequest.Start)
            {
                throw new BadRequest("Project end date can't be earlier than the start date.");
            }

            if (projectRequest.Start > projectRequest.End)
            {
                throw new BadRequest("Project end date can't be earlier than the start date.");
            }

            if (projectRequest.Client <= 0)
            {
                throw new BadRequest("Client must be provided and be a valid integer.");
            }

            if (projectRequest.CampaignType <= 0)
            {
                throw new BadRequest("Campaign type must be provided and be a valid integer.");
            }
        }
    }
}