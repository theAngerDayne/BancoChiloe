using Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Clientes
{
    public interface IClientes
    {
        Task<ServiceResponse<List<Cliente>>> GetClientes();
        Task<ServiceResponse<Cliente>> GetClienteById(int id);
        Task<ServiceResponse<Cliente>> Create(Cliente cliente);
    }
}
