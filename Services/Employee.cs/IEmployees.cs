using GemBox.Models;

namespace GemBox.Services
{
    public interface IEmployeeService
    {
        public IEnumerable<Employee> GetAllEmployees();
        public IEnumerable<Employee> InsertEmployees();
        public MemoryStream ExportExcel();
        public bool UpdateFile(IFormFile file);

        public bool UploadFile();
    }
}