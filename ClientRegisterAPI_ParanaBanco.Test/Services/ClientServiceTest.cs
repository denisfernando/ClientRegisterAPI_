using AutoMapper;
using ClientRegisterAPI_ParanaBanco.Application.DTOs;
using ClientRegisterAPI_ParanaBanco.Application.Services;
using ClientRegisterAPI_ParanaBanco.Application.Services.Interfaces;
using ClientRegisterAPI_ParanaBanco.Domain.Entities;
using ClientRegisterAPI_ParanaBanco.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ClientRegisterAPI_ParanaBanco.Test.Services;

public class ClientTest
{

    [Fact]
    public async void Create_WhenOk()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        ClientDTO client = new ClientDTO { Email = "teste@teste.com", FullName = "Test All Ok" };
        clientRepository.Setup(cr => cr.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult<Client>(null));
        clientRepository.Setup(cr => cr.Create(It.IsAny<Client>())).Returns(Task.FromResult<Client>(new Client(1, "Test All Ok", "teste@teste.com")));
        iMapper.Setup(im => im.Map<Client>(It.IsAny<ClientDTO>())).Returns(new Client("Test All Ok", "teste@teste.com"));
        iMapper.Setup(im => im.Map<ClientDTO>(It.IsAny<Client>())).Returns(new ClientDTO() { Id = 1, FullName = "Test All Ok", Email = "teste@teste.com" });

        //ACT
        var result = await clientService.Create(client);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data.Id);
        Assert.Equal("Test All Ok", result.Data.FullName);
        Assert.Equal("teste@teste.com", result.Data.Email);
    }

    [Fact]
    public async void Create_When_ClientDTO_Is_Null()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        ClientDTO client = null;

        //ACT
        var result = await clientService.Create(client);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("O cliente deve ser informado.", result.Message);
    }

    [Fact]
    public async void Create_When_ClientDTO_Is_Not_Valid()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        ClientDTO client = new ClientDTO { Email = "teste@teste.com", FullName = "" };

        //ACT
        var result = await clientService.Create(client);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("Problemas com a validação dos campos.", result.Message);
    }

    [Fact]
    public async void Create_When_Client_Already_Exists()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        ClientDTO client = new ClientDTO { Email = "teste@teste.com", FullName = "Test Already Exists" };
        clientRepository.Setup(cr => cr.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult<Client>(new Client(1, "Test Already Exists", "teste@teste.com")));


        //ACT
        var result = await clientService.Create(client);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("Este cliente já está cadastrado.", result.Message);
    }

    [Fact]
    public async void GetByEmail_When_AllOk()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        var email = "teste@teste.com";
        //ClientDTO client = new ClientDTO { Email = "teste@teste.com", FullName = "Test Already Exists" };
        clientRepository.Setup(cr => cr.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult<Client>(new Client(1, "Test All Ok", "teste@teste.com")));
        iMapper.Setup(im => im.Map<ClientDTO>(It.IsAny<Client>())).Returns(new ClientDTO() { Id = 1, FullName = "Test All Ok", Email = "teste@teste.com" });


        //ACT
        var result = await clientService.GetByEmail(email);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data.Id);
        Assert.Equal("Test All Ok", result.Data.FullName);
        Assert.Equal("teste@teste.com", result.Data.Email);
    }

    [Fact]
    public async void GetByEmail_When_Email_Is_Null_Or_Empty()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        var email = "";


        //ACT
        var result = await clientService.GetByEmail(email);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("É necessário informar um email para a consulta.", result.Message);
    }

    [Fact]
    public async void GetByEmail_When_Email_Is_Not_Valid()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        var email = "teste@";

        //ACT
        var result = await clientService.GetByEmail(email);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("É necessário informar um email válido para a consulta.", result.Message);
    }

    [Fact]
    public async void GetByEmail_When_Client_Not_Exists()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        var email = "teste@teste.com";
        clientRepository.Setup(cr => cr.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult<Client>(null));


        //ACT
        var result = await clientService.GetByEmail(email);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("Email não encontrado.", result.Message);
    }


    [Fact]
    public async void Update_When_AllOk()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        ClientDTO client = new ClientDTO { Id = 1, Email = "teste@teste.com", FullName = "Test All Ok" };
        clientRepository.Setup(cr => cr.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult<Client>(null));
        clientRepository.Setup(cr => cr.GetById(It.IsAny<int>())).Returns(Task.FromResult<Client>(new Client(1, "Test only", "teste2@teste.com")));
        clientRepository.Setup(cr => cr.Update(It.IsAny<Client>())).Verifiable();
        iMapper.Setup(im => im.Map<ClientDTO, Client>(It.IsAny<ClientDTO>(), It.IsAny<Client>())).Returns(new Client(1, "Test All Ok", "teste@teste.com"));

        //ACT
        var result = await clientService.Update(client);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal("Dados do cliente alterados com sucesso.", result.Message);
    }

    [Fact]
    public async void Update_When_ClientDTO_Is_Null()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        ClientDTO client = null;

        //ACT
        var result = await clientService.Update(client);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("O cliente deve ser informado.", result.Message);
    }

    [Fact]
    public async void Update_When_ClientDTO_Is_Not_Valid()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        ClientDTO client = new ClientDTO { Id = 1, Email = "teste@", FullName = "Test All Ok" };

        //ACT
        var result = await clientService.Update(client);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("Problemas com a validação dos campos.", result.Message);
    }

    [Fact]
    public async void Update_When_Email_Aready_Registered()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        ClientDTO client = new ClientDTO { Id = 1, Email = "teste@teste.com", FullName = "Test All Ok" };
        clientRepository.Setup(cr => cr.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult<Client>(new Client(2, "Test Email Already Registered", "teste@teste.com")));

        //ACT
        var result = await clientService.Update(client);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("Já existe um cliente cadastrado com este email.", result.Message);
    }

    [Fact]
    public async void Update_When_Client_Not_Found()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        ClientDTO client = new ClientDTO { Id = 1, Email = "teste@teste.com", FullName = "Test All Ok" };
        clientRepository.Setup(cr => cr.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult<Client>(null));
        clientRepository.Setup(cr => cr.GetById(It.IsAny<int>())).Returns(Task.FromResult<Client>(null));

        //ACT
        var result = await clientService.Update(client);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("Clinte não encontrado.", result.Message);
    }

    [Fact]
    public async void Delete_When_AllOk()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        var email = "teste@teste.com";
        clientRepository.Setup(cr => cr.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult<Client>(new Client(1, "Test All ok", "teste@teste.com")));
        clientRepository.Setup(cr => cr.Delete(It.IsAny<Client>())).Verifiable();

        //ACT
        var result = await clientService.Delete(email);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal("Cliente excluído com sucesso.", result.Message);
    }

    [Fact]
    public async void Delete_When_Email_Is_Null_Empty_Or_Invalid()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        var email = "teste@";

        //ACT
        var result = await clientService.Delete(email);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("Email inválido.", result.Message);
    }

    [Fact]
    public async void Delete_When_Client_Not_Found()
    {
        //Arrange
        var iMapper = new Mock<IMapper>();
        var clientRepository = new Mock<IClientRepository>();
        var clientService = new ClientService(clientRepository.Object, iMapper.Object);
        var email = "teste@teste.com";
        clientRepository.Setup(cr => cr.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult<Client>(null));

        //ACT
        var result = await clientService.Delete(email);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("Cliente não encontrado.", result.Message);
    }


}