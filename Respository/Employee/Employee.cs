using System.Data;
using GemBox.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GemBox.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        public EmployeeDbContext _DB;
        public EmployeeRepo(EmployeeDbContext dbContext)
        {
            _DB = dbContext;
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _DB.Employees!.FromSqlRaw("EXEC SP_GetAllEmployeeProject");
        }

        public IEnumerable<Employee> InsertEmployees()
        {
            return _DB.Employees!.FromSqlRaw("EXEC SP_INSERTEMPLOYEE").AsEnumerable();
        }

        public bool UDTT(System.Data.DataTable table)
        {
            // string query = "EXEC UDTT @EMPLOYEE";
            // _DB.Database.ExecuteSqlRaw(query,
            //     new SqlParameter("@EMPLOYEE", table));

            string query = "EXEC UDTT @Employees";
            SqlParameter employeesParam = new SqlParameter("@Employees", SqlDbType.Structured);
            employeesParam.Value = table;
            employeesParam.TypeName = "dbo.EmployeeUDTT"; // Replace with your UDTT type name

            Console.WriteLine(_DB.Database.ExecuteSqlRaw(query, employeesParam));

            return true;
        }

        public bool UpdateEmployee(string value, int id, string column)
        {
            string query = "EXEC SP_UPDATEVALUE @ColumnName, @Id, @Value";
            _DB.Database.ExecuteSqlRaw(query,
                new SqlParameter("@ColumnName", column),
                new SqlParameter("@Id", id),
                new SqlParameter("@Value", value));

            return true;
        }
    }
}