using Core.Boundaries.Infrastructure.Interfaces;
using Core.Entities;
using Core.Services.Interfaces;
using Optional;
using Optional.Unsafe;
using Triplex.Validations;

namespace Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFileService _fileService;
        public EmployeeService(IEmployeeRepository employeeRepository, IFileService fileService)
        {
            _employeeRepository = employeeRepository;  
            _fileService = fileService;
        }

        async Task IEmployeeService.Create(Employee employee)
        {
            Arguments.NotNull(employee, nameof(employee));

            employee.PhotoPath = _fileService.GetImagePath(employee.Id);
            bool isEmailTaken = await _employeeRepository.IsEmailTaken(employee.PersonalInformation.Email);

            State.IsFalse(isEmailTaken, $"The email {employee.PersonalInformation.Email} is in use");

            await _employeeRepository.Create(employee);
            await _fileService.SaveImage(employee.PhotoBase64, employee.Id);
        }

        async Task IEmployeeService.Disassociate(Guid id)
        {
            Arguments.NotEmpty(id, nameof(id));

            bool employeeExists = await _employeeRepository.Exists(id);

            State.IsTrue(employeeExists, "Delete while disassociating, employee does not exists");

            await _employeeRepository.MarkAsInactive(id);
        }

        Task<IEnumerable<Employee>> IEmployeeService.GetAll(int currentPage) => _employeeRepository.GetAll(currentPage, "PersonalInformation");

        async Task<Employee> IEmployeeService.GetById(Guid id)
        {
            Arguments.NotEmpty(id, nameof(id));

            Option<Employee> employeeOption = await _employeeRepository.GetById(id, "PersonalInformation");

            State.IsTrue(employeeOption.HasValue, "This employee does not exist");

            return employeeOption.ValueOrFailure();
        }

        async Task IEmployeeService.Update(Employee employee)
        {
            Arguments.NotNull(employee, nameof(employee));

            bool employeeExists = await _employeeRepository.Exists(employee.Id);
            bool isEmailTaken = await _employeeRepository.IsEmailTaken(employee.Id, employee.PersonalInformation.Email);

            State.IsTrue(employeeExists, "Error while updating, employee does not exists");
            State.IsFalse(isEmailTaken, $"The email {employee.PersonalInformation.Email} is in use");

            await _employeeRepository.UpdatePersonalInformation(employee.PersonalInformation);
        }
    }
}
