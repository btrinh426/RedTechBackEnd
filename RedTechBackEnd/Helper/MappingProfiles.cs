using AutoMapper;
using RedTechBackEnd.Dto;
using RedTechBackEnd.Models;

namespace RedTechBackEnd.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<OrderDto, Order>();
        }
    }
}
