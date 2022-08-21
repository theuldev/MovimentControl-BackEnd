using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovimentControl.Api.Entities;
using MovimentControl.Api.Interfaces;
using MovimentControl.Api.Persistence.Repository;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MovimentControl.Api.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository userRepository;
        private readonly ISendGridClient sendGridClient;
        public UserService(IUserRepository repository, ISendGridClient client)
        {
            this.userRepository = repository;
            this.sendGridClient = client;
        }

        public User GetById(int id)
        {
            var model = userRepository.GetById(id);
            return model;
        }

        public User GetUser(string username, string password)
        {
           var model = userRepository.GetUser(username,password);
            return model;
        }

        public async void SendEmail(string tokenFa ,User user)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress("lafifej332@offsala.com", "lafifej332@offsala.com"),
                Subject = "Your Token",
                PlainTextContent = $"Your token is {tokenFa}",

            };
            message.AddTo(user.Email, user.Username);
            await sendGridClient.SendEmailAsync(message);
        }

        public void Update(User user)
        {
            userRepository.UpdateUser(user);
        }
    }
}