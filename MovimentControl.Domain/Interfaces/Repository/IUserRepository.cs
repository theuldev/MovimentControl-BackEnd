using MovimentControl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentControl.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        UserInputModel GetById(int id);
        UserInputModel GetUser(string username, string password);
        void UpdateUser(UserInputModel user);
    }
}
