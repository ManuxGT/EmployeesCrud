using Core.Entities;
using Optional;

namespace Core.Boundaries.Infrastructure.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task MarkAsInactive(Guid id);
        Task UpdatePersonalInformation(PersonalInformation personalInformation);
        Task<bool> Exists(Guid id);
        Task<bool> IsEmailTaken(string email);
        Task<bool> IsEmailTaken(Guid id, string email);
    }
}
