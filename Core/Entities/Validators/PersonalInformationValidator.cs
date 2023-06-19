using FluentValidation;
using System.Text.RegularExpressions;

namespace Core.Entities.Validators
{
    public sealed class PersonalInformationValidator : AbstractValidator<PersonalInformation>
    {
        public static readonly int MinimumLengthName = 2;
        public static readonly int MaxLengthName = 20;

        public static readonly int MinimumLengthLastname = 2;
        public static readonly int MaxLengthLastname = 20;

        public static readonly int MinimumLengthEmail = 5;
        public static readonly int MaxLengthEmail = 50;

        public static readonly int MinimumLengthAddress = 5;
        public static readonly int MaxLengthAddress = 100;

        public static readonly int MinimumLengthPhone = 4;
        public static readonly int MaxLengthPhone = 15;

        public static Regex EmailRegex = new Regex("^(([^<>()\\[\\]\\\\.,;:\\s@\"]+(\\.[^<>()\\[\\]\\\\.,;:\\s@\"]+)*)|(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$");
        public static Regex PhoneNumberRegex = new Regex("^[0-9]*$");

        public PersonalInformationValidator()
        {
            RuleFor(personalInformation => personalInformation.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(personalInformation => personalInformation.Email)
                .MinimumLength(MinimumLengthEmail)
                .MaximumLength(MaxLengthEmail)
                .Matches(EmailRegex)
                .NotNull()
                .NotEmpty();

            RuleFor(personalInformation => personalInformation.PhoneNumber)
                .MinimumLength(MinimumLengthPhone)
                .MaximumLength(MaxLengthPhone)
                .Matches(PhoneNumberRegex)
                .NotNull()
                .NotEmpty();

            RuleFor(personalInformation => personalInformation.Address)
                .MinimumLength(MinimumLengthAddress)
                .MaximumLength(MaxLengthAddress)
                .NotNull()
                .NotEmpty();

            RuleFor(personalInformation => personalInformation.BirthDate)
                .Must(birthDate => birthDate < DateTime.Now);
        }
    }
}
