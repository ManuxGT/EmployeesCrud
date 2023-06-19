using Core.Entities.Validators;
using Core.Enums;
using FluentValidation;

namespace Shared.Models.Employee.Validators
{
    public class EmployeeDTOValidator : AbstractValidator<EmployeeDTO>
    {
        public EmployeeDTOValidator()
        {
            RuleFor(student => student.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(student => student.Name)
                .MinimumLength(EmployeeEntityValidator.MinimumLengthName)
                .MaximumLength(EmployeeEntityValidator.MaxLengthName)
                .NotNull()
                .NotEmpty();

            RuleFor(student => student.Lastname)
                .MinimumLength(EmployeeEntityValidator.MinimumLengthLastname)
                .MaximumLength(EmployeeEntityValidator.MaxLengthLastname)
                .NotNull()
                .NotEmpty();

            RuleFor(student => student.Position)
                .MinimumLength(EmployeeEntityValidator.MinimumLengthPosition)
                .MaximumLength(EmployeeEntityValidator.MaxLengthPosition)
                .NotNull()
                .NotEmpty();

            RuleFor(student => student.Status)
                .Must(status => status != EmployeeStatus.None)
                .NotNull()
                .NotEmpty();
        }
    }
}
