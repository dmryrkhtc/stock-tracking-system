using AutoMapper;
using STS.Application.DTOs.Companies;
using STS.Application.DTOs.Products;
using STS.Application.DTOs.Stock;
using STS.Application.DTOs.StockMovements;
using STS.Application.DTOs.Users;
using STS.Domain.Entities;

namespace STS.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //kaynak--->hedef
            //Stock
            CreateMap<Stock, StockReadDto>();
            CreateMap<StockCreateDto, Stock>();
            CreateMap<StockUpdateDto, Stock>();

            //Company
            CreateMap<Company, CompanyCreateDto>();
            CreateMap<CompanyReadDto, Company>();
            CreateMap<CompanyUpdateDto, Company>();

            //Product
            CreateMap<Product, ProductUpdateDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductReadDto, Product>();

            //User
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateInfoDto, User>();
           

            //StockMovements
            CreateMap<StockMovement, StockMovementReadDto>();
            CreateMap<StockMovementCreateDto, StockMovement>();
            CreateMap<StockMovementUpdateDto, StockMovement>();






        }
    }
}
