using AutoMapper;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Models;
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

            // Course mappings
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ScheduleDates, 
                    opt => opt.MapFrom(src => src.Schedules.Select(s => s.Date).ToList()));

            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();

            // PaymentMethod mappings
            CreateMap<PaymentMethod, PaymentDto>();
            CreateMap<CreatePaymentDto, PaymentMethod>();
            CreateMap<UpdatePaymentDto, PaymentMethod>();

            // User mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(s => s.Role.Name).ToList()))
                .ForMember(dest => dest.Claims, opt => opt.MapFrom(src => src.UserClaims.Select(s => new ClaimDto { Type = s.ClaimType, Value = s.ClaimValue }).ToList()));
            CreateMap<Role, RoleDto>();

            // User CartItem
            CreateMap<CartItem, CartItemResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.CourseName,
                           opt => opt.MapFrom(src => src.Schedule.Course.Name))
                .ForMember(dest => dest.ScheduleDates, 
                           opt => opt.MapFrom(src => new List<DateOnly> { src.Schedule.Date }))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Schedule.Course.Category.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Schedule.Course.Price))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Schedule.Course.ImageUrl));

            // Invoice mappings
            CreateMap<Invoice, InvoiceDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.InvoiceDetails.Sum(s => s.Price)))
                .ForMember(dest => dest.TotalCourse, opt => opt.MapFrom(src => src.InvoiceDetails.Count));

            // Invoice Detail mappings
            CreateMap<InvoiceDetail, InvoiceDetailDto>();
            
            // MyClass mappings
            CreateMap<MyClass, MyClassDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Schedule.Course.Category.Name))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Schedule.Course.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Schedule.Course.ImageUrl))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Schedule.Date));

            CreateMap<CreateMyClassDto, MyClass>();
            CreateMap<UpdateMyClassDto, MyClass>();

            // Schedule mappings
            CreateMap<Schedule, ScheduleDto>()
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name));

            CreateMap<CreateScheduleDto, Schedule>();
            CreateMap<UpdateScheduleDto, Schedule>();
        }
    }
}