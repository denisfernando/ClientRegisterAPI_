using AutoMapper;
using ClientRegisterAPI_ParanaBanco.Application.DTOs;
using ClientRegisterAPI_ParanaBanco.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegisterAPI_ParanaBanco.Application.Mappings
{
    public class DtoToDomainMaping : Profile
    {
        public DtoToDomainMaping()
        {
            CreateMap<ClientDTO, Client>();
        }
    }
}
