using Core.Entities.Validators;
using Core.Enums;
using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public sealed class Employee : Entity
    {
        public string Name { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;

        public string PhotoPath { get; set; } = string.Empty;

        [NotMapped]
        public string PhotoBase64 { get; set; } = string.Empty;

        public string Position { get; set; }

        public DateTime HiredDate { get; set; } = DateTime.Now;

        public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;

        public PersonalInformation PersonalInformation { get; set; }

        public Employee(
            Guid id,
            string name,
            string lastname,
            string position,
            DateTime hiredDate,
            string photoBase64) : base(id)
        {
            Name = name;
            Lastname = lastname;
            Position = position;
            HiredDate = hiredDate;
            PhotoBase64 = photoBase64;

            EmployeeEntityValidator validator = new EmployeeEntityValidator();

            validator.ValidateAndThrow(this);
        }

        public Employee() : base(Guid.NewGuid()) { }
    }
}
