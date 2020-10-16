using System.Threading.Tasks;
using API.Dtos.Cliente;
using API.Services.ClienteService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _clienteService.GetAllClientes());
        }

         [HttpPost]
        public async Task<IActionResult> AddCharacter(AddClienteDto newCliente)
        {
            return Ok(await _clienteService.AddCliente(newCliente));
        }
    }
}