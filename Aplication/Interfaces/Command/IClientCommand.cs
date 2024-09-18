using Domain.Entities;

namespace Application.Interfaces.Command
{
    public interface IClientCommand
    {
        System.Threading.Tasks.Task InsertClient(Client client);
    }
}