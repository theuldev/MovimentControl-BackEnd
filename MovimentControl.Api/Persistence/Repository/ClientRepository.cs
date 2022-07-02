using Microsoft.EntityFrameworkCore;
using MovimentControl.Api.Models;
using MovimentControl.Api.Persistence;

namespace MovimentControl.Api.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly MovimentControlContext repository;

        public ClientRepository(MovimentControlContext _repository)
        {
            repository = _repository;
        }
        public void Add(ClientInputModel client)
        {
            repository.Add(client);
            repository.SaveChanges();
        }

        public void Delete(ClientInputModel client)
        {
            repository.Remove(client);
            repository.SaveChanges();
        }

        public List<ClientInputModel> GetAll()
        {
            return repository._clients.ToList();
        }

        public ClientInputModel GetById(int Id)
        {
            ClientInputModel? clientInputModel = repository._clients.AsNoTracking().Where(x => x.Id == Id).FirstOrDefault();
            if(clientInputModel == null) throw new NullReferenceException("Id não encontrado");
            return clientInputModel;
        }

        public void Update(ClientInputModel client)
        {
            repository.Entry(client).State = EntityState.Modified;
            repository.SaveChanges();
        }
    }
}