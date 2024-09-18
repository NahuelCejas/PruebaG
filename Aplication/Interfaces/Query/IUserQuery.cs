using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Query
{
    public interface IUserQuery
    {
        Task<List<User>> GetListUsers();
        Task<User>  GetUserById(int id);
    }
}