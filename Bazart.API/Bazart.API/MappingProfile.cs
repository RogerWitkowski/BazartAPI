using AutoMapper;
using Bazart.Models.Dto.CategoryDto;
using Bazart.Models.Dto.EventDto;
using Bazart.Models.Dto.ProductDto;
using Bazart.Models.Dto.UserDto;
using Bazart.Models.Model;

namespace Bazart.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Product, ProductDto>()
                .ForMember(u => u.CreatedBy, c => c.MapFrom(s => $"{s.CreatedBy.FirstName} {s.CreatedBy.LastName}"));

            CreateMap<CreateProductDto, Product>()
                .ForPath(p => p.Category.Name, c => c.MapFrom(s => s.CategoryName));

            CreateMap<ProductDto, UpdateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>()
                .ForPath(p => p.CategoryName, c => c.MapFrom(s => s.Category.Name))
                .ForPath(p => p.CategoryId, c => c.MapFrom(s => s.Category.Id)).ReverseMap();

            CreateMap<Event, EventDto>()
                .ForPath(e => e.Country, c => c.MapFrom(s => s.EventDetails.Country))
                .ForPath(e => e.City, c => c.MapFrom(s => s.EventDetails.City))
                .ForPath(e => e.Street, c => c.MapFrom(s => s.EventDetails.Street))
                .ForPath(e => e.HouseOrFlatNumber, c => c.MapFrom(s => s.EventDetails.HouseOrFlatNumber))
                .ForPath(e => e.PostalCode, c => c.MapFrom(s => s.EventDetails.PostalCode))
                .ForPath(e => e.ImageUrl, c => c.MapFrom(s => s.EventDetails.ImageUrl))
                .ForPath(e => e.CreatedBy, c => c.MapFrom(s => $"{s.CreatedBy.FirstName} {s.CreatedBy.LastName}")).ReverseMap();

            CreateMap<Event, CreateEventDto>()
                .ForMember(e => e.Country, c => c.MapFrom(s => s.EventDetails.Country))
                .ForMember(e => e.City, c => c.MapFrom(s => s.EventDetails.City))
                .ForMember(e => e.City, c => c.MapFrom(s => s.EventDetails.Street))
                .ForMember(e => e.HouseOrFlatNumber, c => c.MapFrom(s => s.EventDetails.HouseOrFlatNumber))
                .ForMember(e => e.PostalCode, c => c.MapFrom(s => s.EventDetails.PostalCode))
                .ForMember(e => e.ImageUrl, c => c.MapFrom(s => s.EventDetails.ImageUrl)).ReverseMap();

            CreateMap<Event, UpdateEventDto>()
                .ForMember(e => e.Country, c => c.MapFrom(s => s.EventDetails.Country))
                .ForMember(e => e.City, c => c.MapFrom(s => s.EventDetails.City))
                .ForMember(e => e.City, c => c.MapFrom(s => s.EventDetails.Street))
                .ForMember(e => e.HouseOrFlatNumber, c => c.MapFrom(s => s.EventDetails.HouseOrFlatNumber))
                .ForMember(e => e.PostalCode, c => c.MapFrom(s => s.EventDetails.PostalCode))
                .ForMember(e => e.ImageUrl, c => c.MapFrom(s => s.EventDetails.ImageUrl))
                .ReverseMap();

            CreateMap<RegisterUserDto, User>()
                .ForMember(e => e.Email, c => c.MapFrom(s => s.Email))
                .ForMember(pw => pw.PasswordHash, c => c.MapFrom(s => s.Password))
                .ForPath(u => u.UserName, c => c.MapFrom(s => s.Email))
                .ForMember(a => a.Address, c => c.MapFrom(dto => new Address()
                {
                    Country = dto.Country,
                    City = dto.City,
                    Street = dto.Street,
                    PostalCode = dto.PostalCode,
                    HouseOrFlatNumber = dto.HouseNumber
                })).ReverseMap();

            CreateMap<LoginUserDto, User>()
                .ForMember(e => e.Email, c => c.MapFrom(s => s.Email))
                .ForMember(pw => pw.PasswordHash, c => c.MapFrom(s => s.Password)).ReverseMap();
        }
    }
}