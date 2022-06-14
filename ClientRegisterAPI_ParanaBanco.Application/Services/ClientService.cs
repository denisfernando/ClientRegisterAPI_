using AutoMapper;
using ClientRegisterAPI_ParanaBanco.Application.DTOs;
using ClientRegisterAPI_ParanaBanco.Application.DTOs.Validations;
using ClientRegisterAPI_ParanaBanco.Application.Services.Interfaces;
using ClientRegisterAPI_ParanaBanco.Domain.Entities;
using ClientRegisterAPI_ParanaBanco.Domain.Repositories;
using ClientRegisterAPI_ParanaBanco.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegisterAPI_ParanaBanco.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<ClientDTO>> Create(ClientDTO clientDTO)
        {
            if (clientDTO == null) 
                return ResultService.Fail<ClientDTO>("O cliente deve ser informado.");

            var result = new ClientDtoValidator().Validate(clientDTO);

            if (!result.IsValid)
                return ResultService.RequestError<ClientDTO>("Problemas com a validação dos campos.", result);

            var checkClientInserted = await _clientRepository.GetByEmail(clientDTO.Email);

            if (checkClientInserted != null) 
                return ResultService.Fail<ClientDTO>("Este cliente já está cadastrado.");

            var client = _mapper.Map<Client>(clientDTO);

            var data = await _clientRepository.Create(client);
            return ResultService.Ok<ClientDTO>(_mapper.Map<ClientDTO>(data));
        }

        public async Task<ResultService<ClientDTO>> GetByEmail(string email)
        {
            if (String.IsNullOrEmpty(email)) return ResultService.Fail<ClientDTO>("É necessário informar um email para a consulta.");

            if(!UtilsValidate.IsValidEmail(email)) return ResultService.Fail<ClientDTO>("É necessário informar um email válido para a consulta.");
            
            var client = await _clientRepository.GetByEmail(email);

            if (client == null) return ResultService.Fail<ClientDTO>("Email não encontrado.");

            return ResultService.Ok<ClientDTO>(_mapper.Map<ClientDTO>(client));
        }

        public async Task<ResultService<ICollection<ClientDTO>>> GetClients()
        {
            var clients = await _clientRepository.GetClients();

            return ResultService.Ok<ICollection<ClientDTO>>(_mapper.Map<ICollection<ClientDTO>>(clients));
        }

        public async Task<ResultService> Update(ClientDTO clientDTO)
        {
            if (clientDTO == null) return ResultService.Fail("O cliente deve ser informado.");

            var validation = new ClientDtoValidator().Validate(clientDTO);
            if (!validation.IsValid)
                return ResultService.RequestError("Problemas com a validação dos campos.", validation);

            var emailAreadyRegistered = await _clientRepository.GetByEmail(clientDTO.Email);
            if(emailAreadyRegistered != null && emailAreadyRegistered.Id != clientDTO.Id)
                return ResultService.Fail("Já existe um cliente cadastrado com este email.");

            var client = await _clientRepository.GetById(clientDTO.Id);
            if (client == null) return ResultService.Fail("Clinte não encontrado.");

            client = _mapper.Map<ClientDTO, Client>(clientDTO, client);
            await _clientRepository.Update(client);
            return ResultService.Ok("Dados do cliente alterados com sucesso.");
        }

        public async Task<ResultService> Delete(string email)
        {
            if (String.IsNullOrEmpty(email) || !UtilsValidate.IsValidEmail(email)) 
                return ResultService.Fail("Email inválido.");

            var client = await _clientRepository.GetByEmail(email);

            if (client == null) return ResultService.Fail("Cliente não encontrado.");

            await _clientRepository.Delete(client);

            return ResultService.Ok("Cliente excluído com sucesso.");
        }

      


    }
}
