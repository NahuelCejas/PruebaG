using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Query
{
    public interface IInteractionTypeQuery
    {
        Task<List<InteractionType>> GetListInteractionTypes();
        Task<InteractionType> GetInteractionTypeById(int id);
    }
}