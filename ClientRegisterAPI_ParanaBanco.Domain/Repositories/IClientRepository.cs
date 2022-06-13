using ClientRegisterAPI_ParanaBanco.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegisterAPI_ParanaBanco.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<Client> Create(Client client);
        Task<Client> GetByEmail(string email);
        Task<Client> GetById(int id);
        Task<ICollection<Client>> GetClients();
        Task Update(Client client);
        Task Delete(Client client);


    }
}
