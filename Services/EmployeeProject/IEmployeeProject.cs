using GemBox.Models;

namespace GemBox.Services
{
    public interface IEmployeeProjectService
    {
        public bool AddEmployeeProject(EmployeeProject employeeProject);

        public IEnumerable<EmployeeProject> GetAllEmployeeProject();
    }
}