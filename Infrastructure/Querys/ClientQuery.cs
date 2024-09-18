using Infrastructure.Persistence;
using Application.Interfaces.Query;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class ClientQuery : IClientQuery
    {
        private readonly AppDbContext _context;

        public ClientQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetListClients()
        {
            var result = await _context.Clients.ToListAsync();

            return result;
        }

        public Task<Client> GetClientById(int id)
        {
            var client = _context.Clients.FirstOrDefaultAsync(c => c.ClientID == id);

            return client;
        }
    }
}