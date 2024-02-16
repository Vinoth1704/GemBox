using GemBox.Models;
using GemBox.Services;
using Microsoft.AspNetCore.Mvc;

namespace GemBox.COntrollers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class EmployeeProjectController : ControllerBase
    {
        private IEmployeeProjectService _employeeProjectService;
        public EmployeeProjectController(IEmployeeProjectService employeeProjectService)
        {
            _employeeProjectService = employeeProjectService;
        }

        [HttpPost]
        public IActionResult AddEmployeeProject(EmployeeProject employeeProject)
        {
            return Ok(_employeeProjectService.AddEmployeeProject(employeeProject));
        }

        [HttpGet]
        public IActionResult GetAllEmployeesProject()
        {
            return Ok(_employeeProjectService.GetAllEmployeeProject());
        }

    }
}