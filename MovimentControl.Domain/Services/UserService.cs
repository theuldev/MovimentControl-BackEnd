using FluentValidation.Results;
using MovimentControl.Domain.Interfaces.Repository;
using MovimentControl.Domain.Interfaces.Services;
using MovimentControl.Domain.Models;
using MovimentControl.Domain.Models.Client;
using MovimentControl.Domain.Validations;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentControl.Domain.Services
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

        public UserInputModel GetById(int id)
        {
            var model = userRepository.GetById(id);
            return model;
        }

        public UserInputModel GetUser(string username, string password)
        {
            var model = userRepository.GetUser(username,password);
            return model;
        }

        public async void SendEmail(string tokenFa, UserInputModel user)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress("lafifej332@offsala.com", "lafifej332@offsala.com"),
                Subject = "Your Token",
                PlainTextContent = $"Your token is {tokenFa}"

            };
            message.AddTo(user.Email, user.Username);
            await sendGridClient.SendEmailAsync(message);
        }

        public void Update(UserInputModel user)
        {
            userRepository.UpdateUser(user);
        }
    }
}
