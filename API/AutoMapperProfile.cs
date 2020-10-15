using API.Dtos.Cliente;
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
        }
    }
}