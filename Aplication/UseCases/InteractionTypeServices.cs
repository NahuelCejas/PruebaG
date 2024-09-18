using Application.Interfaces.Query;
using Application.Interfaces.Service;
using Application.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class InteractionTypeServices : IInteractionTypeServices
    {
        private readonly IInteractionTypeQuery _interactionTypeQuery;

        public InteractionTypeServices(IInteractionTypeQuery query)
        {
            _interactionTypeQuery = query;
        }

        public async Task<List<GenericResponse>> GetAll()
        {
            var interactionTypes = await _interactionTypeQuery.GetListInteractionTypes();

            var genericResponses = interactionTypes.Select(it => new GenericResponse
            {
                Id = it.Id,
                Name = it.Name,
            }).ToList();
            return genericResponses;
        }
    }
}