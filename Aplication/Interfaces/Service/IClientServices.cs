using Application.Request;
using Application.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IClientServices
    {
        Task<List<Clients>> GetAll();
        Task<Clients> CreateClient(ClientsRequest request);
    }
}