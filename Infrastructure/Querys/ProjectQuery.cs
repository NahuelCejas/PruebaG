using Infrastructure.Persistence;
using Application.Interfaces.Query;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class ProjectQuery : IProjectQuery
    {
        private readonly AppDbContext _context;

        public ProjectQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Project> GetProjectById(Guid projectId)
        {
            var project = await _context.Set<Project>()
                .Include(p => p.Interactions)
                    .ThenInclude(i => i.InteractionTypes)
                .Include(p => p.Tasks)
                    .ThenInclude(ts => ts.TaskStatus)
                .Include(p => p.Tasks)
                    .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(p => p.ProjectID == projectId);
            return project;
        }

        public async Task<IEnumerable<Project>> GetProjects(string? name, int? campaign, int? client, int? offset, int? size)
        {
            var query = _context.Projects.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.ProjectName.Contains(name));
            }

            if (campaign.HasValue)
            {
                query = query.Where(p => p.CampaignType == campaign.Value);
            }

            if (client.HasValue)
            {
                query = query.Where(p => p.ClientID == client.Value);
            }

            if (offset.HasValue)
            {
                query = query.Skip(offset.Value);
            }

            if (size.HasValue)
            {
                query = query.Take(size.Value);
            }
            return await query.ToListAsync();
        }

        public async Task<Project> GetProjectByName(string name)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectName == name);

            return project;
        }
    }
}