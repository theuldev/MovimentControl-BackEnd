using MovimentControl.Api.Models;

namespace MovimentControl.Api.Interfaces
{
    public interface IClientService
    {
        void Add(ClientInputModel client);
        void Update(ClientInputModel client);
        void Delete(ClientInputModel client);
        ClientInputModel GetById(int Id);
        List<ClientInputModel> GetAll();
    }
}
