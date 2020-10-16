using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Cliente;
using API.Models;

namespace API.Services.ClienteService
{
    public interface IClienteService
    {
        Task<ServiceResponse<List<GetClienteDto>>> GetAllClientes();
        Task<ServiceResponse<List<GetClienteDto>>> AddCliente(AddClienteDto newCliente);
        Task<ServiceResponse<GetClienteDto>> UpdateCharacter(UpdateClienteDto updatedCliente);
        Task<ServiceResponse<List<GetClienteDto>>> DeleteCliente(int id);

        Task<ServiceResponse<GetClienteDto>> GetClienteById(int id);
        Task<ServiceResponse<List<GetClienteDto>>> GetClientesByName(string nombre);
        Task<ServiceResponse<int>> GetCantidadClientes();

    }
}