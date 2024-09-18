using Infrastructure.Persistence;
using Application.Interfaces.Query;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class CampaignTypeQuery : ICampaignTypeQuery
    {
        private readonly AppDbContext _context;

        public CampaignTypeQuery(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<CampaignType>> GetListCampaignTypes()
        {
            var campaignTypes = await _context.CampaignTypes.ToListAsync();

            return campaignTypes;
        }

        public async Task<CampaignType> GetCampaignTypeById(int id)
        {
            var campaignType = _context.CampaignTypes.FirstOrDefaultAsync(c => c.Id == id);
            return await campaignType;
        }
    }
}