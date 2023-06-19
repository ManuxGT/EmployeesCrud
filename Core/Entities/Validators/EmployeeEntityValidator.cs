using FluentValidation;

namespace Core.Entities.Validators
{
    public sealed class EmployeeEntityValidator : AbstractValidator<Employee>
    {
        public static readonly int MinimumLengthName = 2;
        public static readonly int MaxLengthName = 20;

        public static readonly int MinimumLengthLastname = 2;
        public static readonly int MaxLengthLastname = 20;

        public static readonly int MinimumAge = 18;
        public static readonly int MaxAge = 100;

        public static readonly int MinimumLengthPosition = 5;
        public static readonly int MaxLengthPosition = 75;

        public static readonly int MinimumLengthPhotoPath = 10;
        public static readonly int MaxLengthPhotoPath = 500;

        public EmployeeEntityValidator()
        {
            RuleFor(student => student.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(student => student.Name)
                .MinimumLength(MinimumLengthName)
                .MaximumLength(MaxLengthName)
                .NotNull()
                .NotEmpty();

            RuleFor(student => student.Lastname)
                .MinimumLength(MinimumLengthLastname)
                .MaximumLength(MaxLengthLastname)
                .NotNull()
                .NotEmpty();

            RuleFor(student => student.Position)
                .MinimumLength(MinimumLengthPosition)
                .MaximumLength(MaxLengthPosition)
                .NotNull()
                .NotEmpty();

            RuleFor(student => student.Status)
                .Must(status => status != Enums.EmployeeStatus.None)
                .NotNull()
                .NotEmpty();
        }
    }
}
