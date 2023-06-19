using Core.Entities;

namespace Core.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task Create(Employee employee);

        Task Update(Employee employee);

        Task Disassociate(Guid id);

        Task<IEnumerable<Employee>> GetAll(int currentPage);

        Task<Employee> GetById(Guid id);
    }
}
