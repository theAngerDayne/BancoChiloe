using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Cuenta;
using API.Models;
using AutoMapper;
using BancoChiloe.Data;
using BancoChiloe.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services.CuentaService
{
    public class CuentaService : ICuentaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CuentaService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        public async Task<ServiceResponse<List<GetCuentaDto>>> AddCuenta(AddCuentaDto newCuenta)
        {
            ServiceResponse<List<GetCuentaDto>> serviceResponse = new ServiceResponse<List<GetCuentaDto>>();
            Cuenta cuenta = _mapper.Map<Cuenta>(newCuenta);

            await _context.Cuentas.AddAsync(cuenta);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (_context.Cuentas.Select(c => _mapper.Map<GetCuentaDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCuentaDto>>> GetAllCuentas()
        {
            ServiceResponse<List<GetCuentaDto>> serviceResponse = new ServiceResponse<List<GetCuentaDto>>();

            List<Cuenta> dbCuentas = await _context.Cuentas.Include(c => c.Cliente).ToListAsync();

            serviceResponse.Data = (dbCuentas.Select(c => _mapper.Map<GetCuentaDto>(c))).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCuentaDto>> GetCuentaById(int id)
        {
            ServiceResponse<GetCuentaDto> serviceResponse = new ServiceResponse<GetCuentaDto>();
            var dbCuenta = await _context.Cuentas.Include(c => c.Cliente).FirstOrDefaultAsync(c => c.Id == id);

            serviceResponse.Data = _mapper.Map<GetCuentaDto>(dbCuenta);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCuentaDto>>> GetCuentasByIdCliente(int idCliente)
        {
             ServiceResponse<List<GetCuentaDto>> serviceResponse = new ServiceResponse<List<GetCuentaDto>>();

            List<Cuenta> dbCuentas = await _context.Cuentas.Include(c => c.Cliente)
            .Where(i => i.Cliente.Id == idCliente).ToListAsync();

            serviceResponse.Data = (dbCuentas.Select(c => _mapper.Map<GetCuentaDto>(c))).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCuentaDto>>> GetCuentasByTipo(Dtos.Cuenta.TipoCuenta tipoCuenta)
        {
            ServiceResponse<List<GetCuentaDto>> serviceResponse = new ServiceResponse<List<GetCuentaDto>>();

            List<Cuenta> dbCuentas = await _context.Cuentas.Include(c => c.Cliente).Where(t => t.TipoCuenta.Equals(tipoCuenta)).ToListAsync();

            serviceResponse.Data = (dbCuentas.Select(c => _mapper.Map<GetCuentaDto>(c))).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCuentaDto>> UpdateCuenta(UpdateCuentaDto updatedCuenta)
        {
            throw new System.NotImplementedException();
        }
    }
}