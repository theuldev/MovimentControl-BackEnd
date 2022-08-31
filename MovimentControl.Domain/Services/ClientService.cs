using FluentValidation.Results;
using MovimentControl.Domain.Interfaces.Repository;
using MovimentControl.Domain.Interfaces.Services;
using MovimentControl.Domain.Models.Client;
using MovimentControl.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentControl.Domain.Services
{
    public class ClientService :IClientService
    {
        private readonly IClientRepository clientRepository;
        private readonly ClientValidations validator;
        public ClientService(IClientRepository clientRepository,ClientValidations validator)
        {
            this.validator = validator;
            this.clientRepository = clientRepository;
        }

        public void Add(Client client)
        { 
            clientRepository.Add(client);
        }

        public void Delete(Client client)
        {
            clientRepository.Delete(client);
        }

        public List<Client> GetAll()
        {
            var clients = clientRepository.GetAll();
            return clients;
        }

        public Client GetById(int Id)
        {
            var client = clientRepository.GetById(Id);
            return client;
        }

        public void Update(Client client)
        {
            clientRepository.Update(client);
        }
    }
}
