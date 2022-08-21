using MovimentControl.Api.Entities;

namespace MovimentControl.Api.Persistence.Repository
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetUser(string username, string password);
        void UpdateUser(User user);

    }
}
