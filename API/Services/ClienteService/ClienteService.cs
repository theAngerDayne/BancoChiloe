using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Cliente;
using API.Models;
using AutoMapper;
using BancoChiloe.Data;
using BancoChiloe.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services.ClienteService
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ClienteService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        public async Task<ServiceResponse<List<GetClienteDto>>> AddCliente(AddClienteDto newCliente)
        {
            ServiceResponse<List<GetClienteDto>> serviceResponse = new ServiceResponse<List<GetClienteDto>>();
            Cliente cliente = _mapper.Map<Cliente>(newCliente);
            
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (_context.Clientes.Select(c => _mapper.Map<GetClienteDto>(c)
            )).ToList();
            return serviceResponse;

        }

        public Task<ServiceResponse<List<GetClienteDto>>> DeleteCliente(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse<List<GetClienteDto>>> GetAllClientes()
        {
            ServiceResponse<List<GetClienteDto>> serviceResponse = new ServiceResponse<List<GetClienteDto>>();
            List<Cliente> dbClientes = await _context.Clientes.ToListAsync();
            
            serviceResponse.Data = dbClientes.Select(c => _mapper.Map<GetClienteDto>(c)).ToList();

            return serviceResponse;
        }

        public Task<ServiceResponse<GetClienteDto>> UpdateCharacter(UpdateClienteDto updatedCliente)
        {
            throw new System.NotImplementedException();
        }

        Task<ServiceResponse<List<GetClienteDto>>> IClienteService.GetAllClientes()
        {
            throw new System.NotImplementedException();
        }
    }
}