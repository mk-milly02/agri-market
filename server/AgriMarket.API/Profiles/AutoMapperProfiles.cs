using AgriMarket.API.Models.Domain.Auth;
using AgriMarket.API.Models.Domain.Products;
using AgriMarket.API.Models.DTO.Auth;
using AgriMarket.API.Models.DTO.Auth.Requests;
using AgriMarket.API.Models.DTO.Products.Requests;
using AutoMapper;

namespace AgriMarket.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<SignUpRequestDTO, User>().ReverseMap();
            CreateMap<UserProfileDTO, User>().ReverseMap();

            CreateMap<CreateProductDTO, Product>().ReverseMap();
        }
    }
}