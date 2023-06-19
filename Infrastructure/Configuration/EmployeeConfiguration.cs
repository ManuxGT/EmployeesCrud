using Core.Entities;
using Core.Entities.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        void IEntityTypeConfiguration<Employee>.Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(employee => employee.Id);

            builder.Property(employee => employee.Name)
                .IsRequired()
                .HasMaxLength(EmployeeEntityValidator.MaxLengthName);

            builder.Property(employee => employee.Lastname)
                .IsRequired()
                .HasMaxLength(EmployeeEntityValidator.MaxLengthLastname);

            builder.Property(personalInformation => personalInformation.Status)
                .IsRequired()
                .HasColumnType("tinyint");
        }
    }
}
