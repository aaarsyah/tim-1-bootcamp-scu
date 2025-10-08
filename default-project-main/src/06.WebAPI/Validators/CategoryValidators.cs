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
                .Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.ImageUrl))
                .WithMessage("Image URL must be a valid URL");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
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
                .Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.ImageUrl))
                .WithMessage("Image URL must be a valid URL");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
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
                .Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.ImageUrl))
                .WithMessage("Image URL must be a valid URL");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
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
                .Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.ImageUrl))
                .WithMessage("Image URL must be a valid URL");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}