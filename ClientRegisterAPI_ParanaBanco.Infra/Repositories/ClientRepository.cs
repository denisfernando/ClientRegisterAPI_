using ClientRegisterAPI_ParanaBanco.Domain.Entities;
using ClientRegisterAPI_ParanaBanco.Domain.Repositories;
using ClientRegisterAPI_ParanaBanco.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegisterAPI_ParanaBanco.Infra.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ClientRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> Create(Client client)
        {
            _dbContext.Add(client);
            await _dbContext.SaveChangesAsync();
            return client;

        }
        public async Task<Client> GetByEmail(string email)
        {
            return await _dbContext.Client.FirstOrDefaultAsync(x => x.Email.Equals(email));

        }

        public async Task<Client> GetById(int id)
        {
            return await _dbContext.Client.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<Client>> GetClients()
        {
            return await _dbContext.Client.ToListAsync();
        }

        public async Task Update(Client client)
        {
            _dbContext.Update(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Client client)
        {
            _dbContext.Remove(client);
            await _dbContext.SaveChangesAsync();
        }
    }
}
