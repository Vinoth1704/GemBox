using System.Data;
using GemBox.Models;

namespace GemBox.Repository
{
    public interface IEmployeeRepo
    {
        public IEnumerable<Employee> GetAllEmployees();
        public IEnumerable<Employee> InsertEmployees();
        public bool UpdateEmployee(string value, int id, string column);

        public bool UDTT(DataTable table);

    }
}