using AutoMapper;
using MenuWebAPI.DTOs;
using MenuWebAPI.Models;

namespace MenuWebAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Car, CarDto>();
            CreateMap<CarDto, Car>();
        }    
    }
}
