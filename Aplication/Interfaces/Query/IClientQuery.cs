using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Query
{
    public interface IClientQuery
    {
        Task<List<Client>> GetListClients();
        Task<Client> GetClientById(int id);
    }
}