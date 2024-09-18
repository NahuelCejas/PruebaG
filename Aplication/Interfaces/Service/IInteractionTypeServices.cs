using Application.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IInteractionTypeServices
    {
        Task<List<GenericResponse>> GetAll();
    }
}