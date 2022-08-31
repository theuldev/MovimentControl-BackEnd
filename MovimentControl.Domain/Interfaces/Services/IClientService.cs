using MovimentControl.Domain.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentControl.Domain.Interfaces.Services
{
    public interface IClientService
    {
        void Add(Client client);
        void Update(Client client);
        void Delete(Client client);
        Client GetById(int Id);
        List<Client> GetAll();
    }
}
