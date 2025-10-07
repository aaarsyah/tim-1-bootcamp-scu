using AutoMapper;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Mappings
{
    /// <summary>
    /// AutoMapper profile for entity-DTO mappings
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Configure mappings
        /// </summary>
        public MappingProfile()
        {
            // Category mappings
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            // Course mappings
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();

            // PaymentMethod mappings
            CreateMap<PaymentMethod, PaymentDto>();
            CreateMap<CreatePaymentDto, PaymentMethod>();
            CreateMap<UpdatePaymentDto, PaymentMethod>();

            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}