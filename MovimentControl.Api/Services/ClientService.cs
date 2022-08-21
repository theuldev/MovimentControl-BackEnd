using MovimentControl.Api.Interfaces;
using MovimentControl.Api.Models;
using MovimentControl.Api.Repository;

namespace MovimentControl.Api.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public void Add(ClientInputModel client)
        {
            clientRepository.Add(client);
        }

        public void Delete(ClientInputModel client)
        {
        clientRepository.Delete(client);
        }

        public List<ClientInputModel> GetAll()
        {
            var clients = clientRepository.GetAll();
            return clients;
        }

        public ClientInputModel GetById(int Id)
        {
            var client = clientRepository.GetById(Id);
            return client;
        }

        public void Update(ClientInputModel client)
        {
            clientRepository.Update(client);
        }
    }
}
