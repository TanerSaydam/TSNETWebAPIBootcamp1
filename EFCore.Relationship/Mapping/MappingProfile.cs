using AutoMapper;
using EFCore.Relationship.Dtos;
using EFCore.Relationship.Models;

namespace EFCore.Relationship.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<Product, ProductDto>().ForMember(member => member.KDVPrice, options =>
        {
            options.MapFrom(p => p.Price * 1.10m);
        });

        CreateMap<Category, CategoryDto>().ForMember(member => member.CategoryProducts, options =>
        {
            options.MapFrom(p => p.Products!.Select(s => new CategoryProductDto(s.Id, s.Name)));
        });

        CreateMap<ShoppingCart, ShoppingCartDto>();

        CreateMap<CreateShoppingCartDto, ShoppingCart>();

        CreateMap<CreateUserDto, User>();

        CreateMap<User, UserDto>();

        CreateMap<CreateRoleDto, Role>();

        CreateMap<Role, RoleDto>();

        CreateMap<CreateUserRoleDto, UserRole>();

        CreateMap<CreateInvoiceDto, Invoice>()
            .ForMember(m => m.InvoiceDetails, options =>
            {
                options.MapFrom(p => p.Details!.Select(s => new InvoiceDetail()
                {
                    ItemName = s.ItemName
                }));
            });
    }
}


