using AutoMapper;
using MyApp.WebAPI.Models.DTOs;
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
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ScheduleDates, opt => opt.MapFrom(src => src.Schedules.Select(s => s.Date).ToList()));

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