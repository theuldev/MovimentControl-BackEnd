using MovimentControl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentControl.Domain.Interfaces.Services
{
    public interface IUserService
    {
        public void SendEmail(string tokenFa, UserInputModel user);
        public UserInputModel GetById(int id);
        public UserInputModel GetUser(string username, string password);
        public void Update(UserInputModel user);
    }
}
