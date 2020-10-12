using Core;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Clientes
{
    public class ClientesService : IClientes
    {
        private readonly ApplicationDbContext _db;

        public ClientesService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ServiceResponse<Cliente>> Create(Cliente cliente)
        {
            /*Validacion rut unico*/
            var clienteFromdb = await _db.Clientes.Where(r => r.Rut == cliente.Rut.ToUpper().Trim()).FirstOrDefaultAsync();

            if (clienteFromdb != null)
            {
                return 
            }

            
            cliente.Rut.ToUpper();
            cliente.Nombre.ToUpper();
            cliente.Direccion.ToUpper();
            cliente.Apellidos.ToUpper();

            _db.Add(cliente);
            await _db.SaveChangesAsync();
        }

        public async Task<ServiceResponse<Cliente>> GetClienteById(int? id)
        {
            var serviceResponse = new ServiceResponse<Cliente>();
            
            if ( id != null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "La id es invalida";
            }    

            return await _db.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ServiceResponse<List<Cliente>>> GetClientes()
        {
            
            return await _db.Clientes.ToListAsync();
        }
    }
}
