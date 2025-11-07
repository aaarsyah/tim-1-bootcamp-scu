using AutoMapper;
using MyApp.Shared.DTOs;
using MyApp.Domain.Models;

namespace MyApp.Application.Mappings
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
            CreateMap<CreateCategoryRequestDto, Category>();
            CreateMap<UpdateCategoryRequestDto, Category>();

            // Course mappings
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ScheduleDates, 
                    opt => opt.MapFrom(src => src.Schedules.Select(s => s.Date).ToList()));

            CreateMap<CreateCourseRequestDto, Course>();
            CreateMap<UpdateCourseRequestDto, Course>();

            // PaymentMethod mappings
            CreateMap<PaymentMethod, PaymentMethodDto>();
            CreateMap<CreatePaymentMethodRequestDto, PaymentMethod>();
            CreateMap<UpdatePaymentRequestDto, PaymentMethod>();

            // User mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles,
                           opt => opt.MapFrom(src => src.UserRoles.Select(s => s.Role.Name).ToList()))
                .ForMember(dest => dest.Claims,
                           opt => opt.MapFrom(src => src.UserClaims.Select(s => new ClaimDto { Type = s.ClaimType, Value = s.ClaimValue }).ToList()));
            CreateMap<Role, RoleDto>();

            // User CartItem
            CreateMap<CartItem, CartItemResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.CourseName,
                           opt => opt.MapFrom(src => src.Schedule.Course.Name))
                .ForMember(dest => dest.Date, 
                           opt => opt.MapFrom(src => src.Schedule.Date))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Schedule.Course.Category.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Schedule.Course.Price))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Schedule.Course.ImageUrl));

            // Invoice mappings
            CreateMap<Invoice, InvoiceDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User == null ? string.Empty : src.User.Email))
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

            CreateMap<CreateMyClassRequestDto, MyClass>();
            CreateMap<UpdateMyClassRequestDto, MyClass>();

            // Schedule mappings
            CreateMap<Schedule, ScheduleDto>()
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name));

            CreateMap<CreateScheduleRequestDto, Schedule>();
            CreateMap<UpdateScheduleRequestDto, Schedule>();
        }
    }
}