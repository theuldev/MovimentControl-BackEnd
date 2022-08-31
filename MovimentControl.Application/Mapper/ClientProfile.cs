using AutoMapper;
using MovimentControl.Domain.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentControl.Application.Mapper
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientInputModel, Client>();
            CreateMap<Client,ClientViewModel>();    
        }
    }
}
