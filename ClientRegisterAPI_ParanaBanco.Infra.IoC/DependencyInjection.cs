using ClientRegisterAPI_ParanaBanco.Application.Mappings;
using ClientRegisterAPI_ParanaBanco.Application.Services;
using ClientRegisterAPI_ParanaBanco.Application.Services.Interfaces;
using ClientRegisterAPI_ParanaBanco.Domain.Repositories;
using ClientRegisterAPI_ParanaBanco.Infra.Context;
using ClientRegisterAPI_ParanaBanco.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegisterAPI_ParanaBanco.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IClientRepository, ClientRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDtoMaping));
            services.AddScoped<IClientService, ClientService>();

            return services;
        }

    }
}
