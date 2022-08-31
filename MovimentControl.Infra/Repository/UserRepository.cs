using Microsoft.EntityFrameworkCore;
using MovimentControl.Domain.Interfaces.Repository;
using MovimentControl.Domain.Models;
namespace MovimentControl.Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Context repository;
        public UserRepository(Context _repository)
        {
            repository = _repository;
        }
        public UserInputModel GetById(int id)
        {
            UserInputModel user = repository._users.Where(o => o.Id == id).FirstOrDefault();
            return user == null ? throw new NullReferenceException("Id não encontrado") : user;
        }
        public UserInputModel GetUser(string username,string password)
        {
            UserInputModel user = repository._users.AsNoTracking().Where(u => u.Username == username && u.Password == password).FirstOrDefault();
            return user == null ? throw new NullReferenceException("Username ou Password não encontrado") : user;
        }

        public void UpdateUser(UserInputModel user)
        {
            user.LoggedTime = DateTime.Now;
            repository.Entry(user).State = EntityState.Modified;
            repository.SaveChanges();
        }
    }
}
