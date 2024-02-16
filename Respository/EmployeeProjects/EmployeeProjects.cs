using GemBox.Models;

namespace GemBox.Repository
{
    public class EmployeeProjectsRepo : IEmployeeProjectsRepo
    {
        public EmployeeDbContext _DB;
        public EmployeeProjectsRepo(EmployeeDbContext dbContext)
        {
            _DB = dbContext;
        }
        public bool AddEmployeeProject(EmployeeProject employeeProject)
        {
            _DB.Add(employeeProject);
            _DB.SaveChanges();
            return true;
        }

        public IEnumerable<EmployeeProject> GetAllEmployeeProject()
        {
            return from Employee in _DB.EmployeeProjects select Employee;
        }
    }
}