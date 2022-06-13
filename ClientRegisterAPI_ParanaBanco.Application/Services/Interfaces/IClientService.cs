using ClientRegisterAPI_ParanaBanco.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegisterAPI_ParanaBanco.Application.Services.Interfaces
{
    public interface IClientService
    {
        Task<ResultService<ClientDTO>> Create(ClientDTO clientDTO);
        Task<ResultService<ClientDTO>> GetByEmail(string email);
        Task<ResultService<ICollection<ClientDTO>>> GetClients();
        Task<ResultService> Update(ClientDTO clientDTO);
        Task<ResultService> Delete(string email);
    }
}
