using Core.Boundaries.Infrastructure.Interfaces;
using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(SqlContext context) : base(context)
        {
        }

        async Task<bool> IEmployeeRepository.Exists(Guid id) => await _context.Employee.AnyAsync(employee => employee.Id == id);
        async Task<bool> IEmployeeRepository.IsEmailTaken(string email) => await _context.PersonalInformation.AnyAsync(personalInfo => personalInfo.Email == email);
        async Task<bool> IEmployeeRepository.IsEmailTaken(Guid employeeId, string email) => await _context.PersonalInformation.AnyAsync(personalInformation => personalInformation.Email == email && personalInformation.EmployeeId != employeeId);

        async Task IEmployeeRepository.MarkAsInactive(Guid id)
        {
            Employee employee = await _context.Employee.FirstAsync(employee => employee.Id == id);
            employee.Status = EmployeeStatus.Inactive;

            _context.Employee.Update(employee);

            await _context.SaveChangesAsync();
        }

        async Task IEmployeeRepository.UpdatePersonalInformation(PersonalInformation personalInformation)
        {
            PersonalInformation personalInfo = await _context.PersonalInformation.FirstAsync(info => info.Id == personalInformation.Id);
            personalInfo.Email = personalInformation.Email;
            personalInfo.PhoneNumber = personalInformation.PhoneNumber;

            _context.PersonalInformation.Update(personalInfo);

            await _context.SaveChangesAsync();
        }
    }
}
