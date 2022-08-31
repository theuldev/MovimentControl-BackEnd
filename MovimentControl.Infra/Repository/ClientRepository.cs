
using Microsoft.EntityFrameworkCore;
using MovimentControl.Domain.Interfaces.Repository;
using MovimentControl.Domain.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentControl.Infra.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly Context repository;

        public ClientRepository(Context _repository)
        {
            repository = _repository;
        }
        public void Add(Client client)
        {

            repository.Add(client);
            repository.SaveChanges();
        }

        public void Delete(Client client)
        {
            repository.Remove(client);
            repository.SaveChanges();
        }

        public List<Client> GetAll()
        {
            return repository._clients.ToList();
        }

        public Client GetById(int Id)
        {
            Client? clientInputModel = repository._clients.AsNoTracking().Where(x => x.Id == Id).FirstOrDefault();
            if (clientInputModel == null) throw new NullReferenceException("Id não encontrado");
            return clientInputModel;
        }

        public void Update(Client client)
        {
            repository.Entry(client).State = EntityState.Modified;
            repository.SaveChanges();
        }
    }
}
