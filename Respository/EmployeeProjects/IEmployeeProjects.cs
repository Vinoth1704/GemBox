using GemBox.Models;

namespace GemBox.Repository
{
    public interface IEmployeeProjectsRepo
    {
        public bool AddEmployeeProject(EmployeeProject employeeProject);

        public IEnumerable<EmployeeProject> GetAllEmployeeProject();
    }
}