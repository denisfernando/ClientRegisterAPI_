using ClientRegisterAPI_ParanaBanco.Application.DTOs;
using ClientRegisterAPI_ParanaBanco.Application.Services;
using ClientRegisterAPI_ParanaBanco.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ClientRegisterAPI_ParanaBanco.Test;

public class ClientTest
{
    private readonly IClientService _clientService;
    public ClientTest(IClientService clientService)
    {
        _clientService = clientService;
    }

    [Fact]
    public async void testCreate_WhenAllOk()
    {
        ///Arrange
        var clientService = new Mock<IClientService>();
        ClientDTO client = new ClientDTO { Email = "teste@teste.com", FullName = "Test All Ok" };
        var clientTest = new ClientDTO { Email = client.Email, FullName = client.FullName, Id = 1 };
        var resultTest = ResultService.Ok<ClientDTO>(clientTest);
        clientService.Setup(cl => cl.Create(It.IsAny<ClientDTO>())).Returns(Task.FromResult(resultTest));


        //ACT
        var clientResult = await _clientService.Create(client);
        Assert.True(clientResult.IsSuccess);
        Assert.Equal(clientResult.Data.FullName, client.FullName);
        Assert.Equal(clientResult.Data.Email, client.Email);
    }

    [Fact]
    public async void testCreate_WhenInvalidEmail()
    {
        ClientDTO client = new ClientDTO { Email = "test@", FullName = "Teste Invalid Email" };
        var result = await _clientService.Create(client);

       Assert.False(result.IsSuccess);
    }

    [Fact]
    public async void testCreate_WhenFullNameIsNullOrEmpty()
    {
        ClientDTO client = new ClientDTO { Email = "teste@test.com", FullName = "" };
        var result = await _clientService.Create(client);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async void testCreate_WhenEmailIsNullOrEmpty()
    {
        ClientDTO client = new ClientDTO { Email = "", FullName = "Test Email Null or Empty" };
        var result = await _clientService.Create(client);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async void testGetByEmail_WhenOk()
    {
        var result = await _clientService.GetByEmail("teste@teste.com");
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async void testGetByEmail_WhenNoResult()
    {
        var result = await _clientService.GetByEmail("teste10000@teste.com");
        Assert.Null(result.Data);
    }

    [Fact]
    public async void testGetClients()
    {
        var result = await _clientService.GetClients();

        Assert.NotNull(result.Data);
    }

    [Fact]
    public async void testUpdate_WhenOk()
    {
        var client = new ClientDTO { Id = 4, Email = "teste31@teste.com", FullName = "Nome do Fulano" };
        var result = await _clientService.Update(client);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void testUpdate_WhenIdNotExist()
    {
        var client = new ClientDTO { Id = 9999, Email = "teste31@notexists.com", FullName = "Nome do Fulano" };
        var result = await _clientService.Update(client);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async void testUpdate_WhenEmailAlreadyExist()
    {
        var client = new ClientDTO { Id = 9999, Email = "teste31@teste.com", FullName = "Nome do Fulano" };
        var result = await _clientService.Update(client);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async void testDelete_WhenOk()
    {
        var result = await _clientService.Delete("teste@teste.com");
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async void testDelete_WhenEmailNoExist()
    {
        var result = await _clientService.Delete("teste@teste.com");
        Assert.True(result.IsSuccess);
    }

}