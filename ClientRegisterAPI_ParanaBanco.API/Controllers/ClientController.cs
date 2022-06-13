using ClientRegisterAPI_ParanaBanco.Application.DTOs;
using ClientRegisterAPI_ParanaBanco.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClientRegisterAPI_ParanaBanco.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController (IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ClientDTO client)
        {
            var result = await _clientService.Create(client);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{email}")]
        public async Task<ActionResult> GetByEmail(string email)
        {
            var result = await _clientService.GetByEmail(email);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        } 
        
        [HttpGet]
        public async Task<ActionResult> GetClients()
        {
            var result = await _clientService.GetClients();

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ClientDTO client)
        {
            var result = await _clientService.Update(client);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        } 
        
        [HttpDelete]
        [Route("{email}")]
        public async Task<ActionResult> Delete(string email)
        {
            var result = await _clientService.Delete(email);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }



    }
}
