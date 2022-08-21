using Microsoft.EntityFrameworkCore;
using MovimentControl.Api.Entities;

namespace MovimentControl.Api.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MovimentControlContext repository;
       public UserRepository(MovimentControlContext _repository) 
        {
            repository = _repository;
        }
        public User GetById(int id) 
        {
             User user =  repository._users.Where(o => o.Id == id).FirstOrDefault();
            return user == null ? throw new NullReferenceException("Username não encontrado") : user; 
        }
        public User GetUser(string username, string password)
        {
            User user = repository._users.AsNoTracking().Where(u => u.Username == username && u.Password == password).FirstOrDefault();
            return user == null ? throw new NullReferenceException("Username não encontrado") : user;
        }

        public void UpdateUser(User user)
        {
            user.LoggedTime = DateTime.Now;
            repository.Entry(user).State = EntityState.Modified;
            repository.SaveChanges();
        }
    }
}
