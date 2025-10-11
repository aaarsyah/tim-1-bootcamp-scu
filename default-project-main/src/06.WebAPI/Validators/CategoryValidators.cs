using FluentValidation;
using MyApp.WebAPI.Models.DTOs;

namespace MyApp.WebAPI.Validators
{
    /// <summary>
    /// Validator for creating categories
    /// </summary>
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        /// <summary>
        /// Constructor with validation rules
        /// </summary>
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .Length(2, 100).WithMessage("Category name must be between 2 and 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Image Category is required");
        }

    }

    /// <summary>
    /// Validator for updating categories
    /// </summary>
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        /// <summary>
        /// Constructor with validation rules
        /// </summary>
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .Length(2, 100).WithMessage("Category name must be between 2 and 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Image Category is required");
        }
    }

    /// <summary>
    /// Validator for creating products
    /// </summary>
    public class CreateCourseDtoValidator : AbstractValidator<CreateCourseDto>
    {
        /// <summary>
        /// Constructor with validation rules
        /// </summary>
        public CreateCourseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Course name is required")
                .Length(2, 200).WithMessage("Course name must be between 2 and 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0")
                .LessThanOrEqualTo(999999.99m).WithMessage("Price cannot exceed 999,999.99");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Valid category is required");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Image Course is required");
        }
    }

    /// <summary>
    /// Validator for updating products
    /// </summary>
    public class UpdateCourseDtoValidator : AbstractValidator<UpdateCourseDto>
    {
        /// <summary>
        /// Constructor with validation rules
        /// </summary>
        public UpdateCourseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Course name is required")
                .Length(2, 200).WithMessage("Course name must be between 2 and 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0")
                .LessThanOrEqualTo(999999.99m).WithMessage("Price cannot exceed 999,999.99");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Valid category is required");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Image Course is required");
        }
    }

    //Payment Method
    public class CreatePaymentDtoValidator : AbstractValidator<CreatePaymentDto>
    {
        /// <summary>
        /// Constructor with validation rules
        /// </summary>
        public CreatePaymentDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Payment name is required")
                .Length(2, 100).WithMessage("Payment name must be between 2 and 100 characters");

            RuleFor(x => x.LogoUrl)
                .NotEmpty().WithMessage("Logo Payment is required");
        }
    }

    /// <summary>
    /// Validator for updating payment method
    /// </summary>
    public class UpdatePaymentDtoValidator : AbstractValidator<UpdatePaymentDto>
    {
        /// <summary>
        /// Constructor with validation rules
        /// </summary>
        public UpdatePaymentDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Payment name is required")
                .Length(2, 100).WithMessage("Payment name must be between 2 and 100 characters");

            RuleFor(x => x.LogoUrl)
                .NotEmpty().WithMessage("Logo Payment is required");
        }
    }
   
    //MyClass
    public class CreateMyClassDtoValidator : AbstractValidator<CreateMyClassDto>
    {
        /// <summary>
        /// Constructor with validation rules
        /// </summary>
        public CreateMyClassDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required");

            RuleFor(x => x.ScheduleId)
                .NotEmpty().WithMessage("ScheduleId is required");
        }
    }

    /// <summary>
    /// Validator for updating payment method
    /// </summary>
    public class UpdateMyClassDtoValidator : AbstractValidator<UpdateMyClassDto>
    {
        /// <summary>
        /// Constructor with validation rules
        /// </summary>
        public UpdateMyClassDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required");

            RuleFor(x => x.ScheduleId)
                .NotEmpty().WithMessage("ScheduleId is required");
        }
    }

    //Schedule
    public class CreateScheduleDtoValidator : AbstractValidator<CreateScheduleDto>
    {
        /// <summary>
        /// Constructor with validation rules
        /// </summary>
        public CreateScheduleDtoValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required");

            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("CourseId is required");
        }
    }

    /// <summary>
    /// Validator for updating payment method
    /// </summary>
    public class UpdateScheduleDtoValidator : AbstractValidator<UpdateScheduleDto>
    {
        /// <summary>
        /// Constructor with validation rules
        /// </summary>
        public UpdateScheduleDtoValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required");

            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("CourseId is required");
        }
    }

}