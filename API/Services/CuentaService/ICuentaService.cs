using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Cuenta;
using API.Models;

namespace API.Services.CuentaService
{
    public interface ICuentaService
    {

        Task<ServiceResponse<List<GetCuentaDto>>> GetAllCuentas();
        Task<ServiceResponse<GetCuentaDto>> GetCuentaById(int id);

        Task<ServiceResponse<List<GetCuentaDto>>> GetCuentasByTipo(TipoCuenta tipoCuenta);

        Task<ServiceResponse<List<GetCuentaDto>>> GetCuentasByIdCliente(int idCliente);

        Task<ServiceResponse<List<GetCuentaDto>>> AddCuenta(AddCuentaDto newCuenta);

        Task<ServiceResponse<GetCuentaDto>> UpdateCuenta(UpdateCuentaDto updatedCuenta);
        /*
        
        modificar
        desactivar
        */
    }
}