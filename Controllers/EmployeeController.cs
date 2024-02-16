using GemBox.Services;
using Microsoft.AspNetCore.Mvc;

namespace GemBox.COntrollers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult InsertEmployee()
        {
            return Ok(_employeeService.InsertEmployees());
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(_employeeService.GetAllEmployees());
        }

        [HttpGet]
        public IActionResult ExportExcel()
        {
            return new FileStreamResult(_employeeService.ExportExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "sample.xlsx"
            };
        }

        [HttpPost]
        public IActionResult UpdateFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                _employeeService.UpdateFile(file);
                return Ok();
            }
            else
                throw new FileNotFoundException();
        }

        [HttpGet]
        public IActionResult UploadFile()
        {
            return Ok(_employeeService.UploadFile());
        }

    }
}