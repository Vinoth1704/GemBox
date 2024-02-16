using GemBox.Models;
using GemBox.Repository;

namespace GemBox.Services
{
    public class EmployeeProjectService : IEmployeeProjectService
    {
        public IEmployeeProjectsRepo _employeeProjectsRepo;
        public EmployeeProjectService(IEmployeeProjectsRepo employeeProjectsRepo)
        {
            _employeeProjectsRepo = employeeProjectsRepo;
        }
        public bool AddEmployeeProject(EmployeeProject employeeProject)
        {
            return _employeeProjectsRepo.AddEmployeeProject(employeeProject);
        }

        public IEnumerable<EmployeeProject> GetAllEmployeeProject()
        {
            return _employeeProjectsRepo.GetAllEmployeeProject();
        }
    }
}