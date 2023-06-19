namespace Shared.Models.PersonalInformation
{
    public class PersonalInformationDTO
    {
        public Guid Id { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public Core.Entities.PersonalInformation ToEntity(Guid EmployeeId)
        {
            return new Core.Entities.PersonalInformation(Id, Email, PhoneNumber, Address, EmployeeId);
        }

        public static PersonalInformationDTO FromEntity(Core.Entities.PersonalInformation personalInformation)
        {
            return new PersonalInformationDTO()
            {
                Id = personalInformation.Id,
                Email = personalInformation.Email,
                PhoneNumber = personalInformation.PhoneNumber,
                BirthDate = personalInformation.BirthDate
            };
        }
    }
}
