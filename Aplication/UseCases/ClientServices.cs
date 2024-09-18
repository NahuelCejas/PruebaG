using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Interfaces.Service;
using Application.Request;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class ClientServices : IClientServices
    {
        private readonly IClientCommand _clientCommand;
        private readonly IClientQuery _clientQuery;

        public ClientServices(IClientQuery query, IClientCommand command)
        {
            _clientQuery = query;
            _clientCommand = command;
        }

        public async Task<Clients> CreateClient(ClientsRequest request)
        {
            //Validacion de datos ingresados no nulos ni vacios
            ValidateClientRequest(request);

            var client = new Client
            {
                Name = request.Name,
                Email = request.Email,
                Company = request.Company,
                Phone = request.Phone,
                Address = request.Address,
                CreateDate = DateTime.Now,
            };
            await _clientCommand.InsertClient(client);
            return new Clients
            {
                Id = client.ClientID,
                Name = client.Name,
                Email = client.Email,
                Company = client.Company,
                Phone = client.Phone,
                Address = client.Address,
            };
        }
        public async Task<List<Clients>> GetAll()
        {
            var clients = await _clientQuery.GetListClients();
            var result = clients.Select(c => new Clients
            {
                Id = c.ClientID,
                Name = c.Name,
                Email = c.Email,
                Company = c.Company,
                Phone = c.Phone,
                Address = c.Address,
            }).ToList();
            return result;
        }

        // Metodo privado que valida datos ingresados no nulos ni vacios
        private void ValidateClientRequest(ClientsRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new BadRequest("Client name cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(request.Email))
                throw new BadRequest("Client email cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(request.Company))
                throw new BadRequest("Client company cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(request.Phone))
                throw new BadRequest("Client phone number cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(request.Address))
                throw new BadRequest("Client address cannot be null or empty.");
        }
    }
}