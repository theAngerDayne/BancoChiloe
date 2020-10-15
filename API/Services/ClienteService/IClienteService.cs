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

        //buscar cliente por id
        //metodo que busque un listado de clientes en base a un nombre
        //metodo q entregue la cantidad de clientes en el sistema
    }
}