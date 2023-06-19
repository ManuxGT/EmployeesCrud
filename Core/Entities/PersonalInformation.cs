using Core.Entities.Validators;
using FluentValidation;

namespace Core.Entities
{
    public sealed class PersonalInformation : Entity
    {
        public DateTime BirthDate { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public PersonalInformation(
            Guid id, 
            string email, 
            string phoneNumber,
            string address,
            Guid employeeId) : base(id)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            EmployeeId = employeeId;

            PersonalInformationValidator validator = new PersonalInformationValidator();

            validator.ValidateAndThrow<PersonalInformation>(this);
        }
    }
}
