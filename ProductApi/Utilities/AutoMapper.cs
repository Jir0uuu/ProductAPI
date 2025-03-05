using ProductApi.Models;
using AutoMapper;
using ProductApi.Models.DTO;

namespace ProductApi.Utilities
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<ProductDTO, Product>();
            CreateMap<RegisterDTO, Users>();
        }
    }
}
