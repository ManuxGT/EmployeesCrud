using Core.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Employee;
using Triplex.Validations;

namespace CrudWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IValidator<EmployeeDTO> _employeeValidator;
        private readonly IFileService _fileService;
        public EmployeeController(IEmployeeService employeeService, IValidator<EmployeeDTO> employeeValidator, IFileService fileService) 
        {
            _employeeService = employeeService;
            _employeeValidator = employeeValidator;
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDTO createEmployeeDto) 
        {
            Arguments.NotNull(createEmployeeDto, nameof(createEmployeeDto));
            _employeeValidator.ValidateAndThrow(createEmployeeDto);

            Core.Entities.Employee employee = createEmployeeDto.ToEntity();
            await _employeeService.Create(employee);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EmployeeDTO updateEmployeeDto)
        {
            Arguments.NotNull(updateEmployeeDto, nameof(updateEmployeeDto));
            _employeeValidator.ValidateAndThrow(updateEmployeeDto);

            Core.Entities.Employee employee = updateEmployeeDto.ToEntity();
            await _employeeService.Update(employee);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]int currentPage)
        {
            IEnumerable<Core.Entities.Employee> employees = await _employeeService.GetAll(currentPage);

            IEnumerable<EmployeeDTO> employeesRows = employees.Select(employee => EmployeeDTO.FromEntity(employee));

            return Ok(employeesRows);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            Arguments.NotEmpty(id, nameof(id));

            Core.Entities.Employee employee = await _employeeService.GetById(id);

            EmployeeDTO employeeDto = EmployeeDTO.FromEntity(employee);
            await employeeDto.SetImageBase64(_fileService);

            return Ok(employeeDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Arguments.NotEmpty(id, nameof(id));

            await _employeeService.Disassociate(id);

            return Ok();
        }

    }
}
