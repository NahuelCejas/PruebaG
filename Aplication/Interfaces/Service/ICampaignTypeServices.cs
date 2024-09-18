using Application.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface ICampaignTypeServices
    {
        Task<List<GenericResponse>> GetAll();
    }
}