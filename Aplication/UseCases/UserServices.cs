using Application.Interfaces.Query;
using Application.Interfaces.Service;
using Application.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class UserServices : IUserServices
    {
        private readonly IUserQuery _userQuery;

        public UserServices(IUserQuery query)
        {
            _userQuery = query;
        }

        public async Task<List<Users>> GetAll()
        {
            var users = await _userQuery.GetListUsers();
            var result = users.Select(u => new Users
            {
                UserID = u.UserID,
                Name = u.Name,
                Email = u.Email,
            }).ToList();
            return result;
        }
    }
}