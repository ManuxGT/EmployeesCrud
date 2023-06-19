using Core.Entities.Validators;
using FluentValidation;

namespace Shared.Models.PersonalInformation.Validators
{
    public sealed class PersonalInformationDTOValidator : AbstractValidator<PersonalInformationDTO>
    {
        public PersonalInformationDTOValidator()
        {
            RuleFor(personalInformation => personalInformation.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(personalInformation => personalInformation.Email)
                .MinimumLength(PersonalInformationValidator.MinimumLengthEmail)
                .MaximumLength(PersonalInformationValidator.MaxLengthEmail)
                .Matches(PersonalInformationValidator.EmailRegex)
                .NotNull()
                .NotEmpty();

            RuleFor(personalInformation => personalInformation.PhoneNumber)
                .MinimumLength(PersonalInformationValidator.MinimumLengthPhone)
                .MaximumLength(PersonalInformationValidator.MaxLengthPhone)
                .Matches(PersonalInformationValidator.PhoneNumberRegex)
                .NotNull()
                .NotEmpty();

            RuleFor(personalInformation => personalInformation.Address)
                .MinimumLength(PersonalInformationValidator.MinimumLengthAddress)
                .MaximumLength(PersonalInformationValidator.MaxLengthAddress)
                .NotNull()
                .NotEmpty();

            RuleFor(personalInformation => personalInformation.BirthDate)
                .Must(birthDate => birthDate < DateTime.Now);
        }
    }
}
