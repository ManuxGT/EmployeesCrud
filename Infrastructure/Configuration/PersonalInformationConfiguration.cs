using Core.Entities;
using Core.Entities.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class PersonalInformationConfiguration : IEntityTypeConfiguration<PersonalInformation>
    {
        void IEntityTypeConfiguration<PersonalInformation>.Configure(EntityTypeBuilder<PersonalInformation> builder)
        {
            builder.HasKey(personalInformation => personalInformation.Id);

            builder.HasIndex(personalInformation => personalInformation.Email)
                .IsUnique(true);

            builder.Property(personalInformation => personalInformation.Id);

            builder.Property(personalInformation => personalInformation.PhoneNumber)
                .IsRequired()
                .HasMaxLength(PersonalInformationValidator.MaxLengthPhone);

            builder.Property(personalInformation => personalInformation.Email)
                .IsRequired()
                .HasMaxLength(PersonalInformationValidator.MaxLengthEmail);

            builder.HasOne(personalInfo => personalInfo.Employee)
                .WithOne(employee => employee.PersonalInformation)
                .HasForeignKey<PersonalInformation>(personalInfo => personalInfo.EmployeeId);
        }
    }
}
