using MovimentControl.Api.Entities;

namespace MovimentControl.Api.Interfaces
{
    public interface IUserService
    {
        public void SendEmail(string tokenFa,User user);
        public User GetById(int id);
        public User GetUser(string username, string password);
        public void Update(User user);
    }
}
