using AutoMapper;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Models.Entities;

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

            // Product mappings
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            
            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();

            // Checkout mappings
            CreateMap<CartItem, CartItemResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Schedule.Course.Name));
        }
    }
}