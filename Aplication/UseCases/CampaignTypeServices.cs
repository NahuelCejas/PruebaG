using Application.Interfaces.Query;
using Application.Interfaces.Service;
using Application.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CampaignTypeServices : ICampaignTypeServices
    {
        private readonly ICampaignTypeQuery _campaignTypeQuery;

        public CampaignTypeServices(ICampaignTypeQuery query)
        {
            _campaignTypeQuery = query;
        }

        public async Task<List<GenericResponse>> GetAll()
        {
            var campaignTypes = await _campaignTypeQuery.GetListCampaignTypes();
            var genericResponses = campaignTypes.Select(ct => new GenericResponse
            {
                Id = ct.Id,
                Name = ct.Name
            }).ToList();
            return genericResponses;
        }
    }
}