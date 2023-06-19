using Core.Enums;
using Core.Services.Interfaces;
using Shared.Models.PersonalInformation;

namespace Shared.Models.Employee
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;

        public string PhotoBase64 { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

        public DateTime HiredDate { get; set; } = DateTime.Now;

        public EmployeeStatus Status { get; set; }

        public PersonalInformationDTO PersonalInformation { get; set; } = new();

        public Core.Entities.Employee ToEntity()
        {
            Core.Entities.Employee mappedEntity = new Core.Entities.Employee(Id, Name, Lastname, Position, HiredDate, PhotoBase64);
            mappedEntity.PersonalInformation = PersonalInformation.ToEntity(Id);

            return mappedEntity;
        }

        public static EmployeeDTO FromEntity(Core.Entities.Employee employee)
        {
            return new EmployeeDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                Lastname = employee.Lastname,
                Position = employee.Position,
                HiredDate = employee.HiredDate,
                Status = employee.Status,
                PhotoPath = employee.PhotoPath,
                PersonalInformation = PersonalInformationDTO.FromEntity(employee.PersonalInformation)
            };
        }

        public async Task SetImageBase64(IFileService fileService) 
        {
            PhotoBase64 = await fileService.GetImageBase64StringAsync(PhotoPath);
        }
    }
}
