using API.Dtos.Cliente;
using API.Dtos.Cuenta;
using AutoMapper;
using BancoChiloe.Models;

namespace API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cliente, GetClienteDto>();
            CreateMap<AddClienteDto, Cliente>();

            CreateMap<Cuenta, GetCuentaDto>();
            CreateMap<AddCuentaDto, Cuenta>();
        }
    }
}