using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Query
{
    public interface ICampaignTypeQuery
    {
        Task<List<CampaignType>> GetListCampaignTypes();
        Task<CampaignType> GetCampaignTypeById(int id);
    }
}