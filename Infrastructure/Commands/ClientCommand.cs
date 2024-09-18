using Infrastructure.Persistence;
using Application.Interfaces.Command;
using Domain.Entities;

namespace Infrastructure.Command
{
    public class ClientCommand : IClientCommand
    {
        private readonly AppDbContext _context;

        public ClientCommand(AppDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task InsertClient(Client client)
        {
            _context.Add(client);
            await _context.SaveChangesAsync();
        }
    }
}