using System.Data;
using System.Net;
using System.Net.Http.Headers;
using GemBox.Models;
using GemBox.Repository;
using GemBox.Spreadsheet;
using Renci.SshNet;

namespace GemBox.Services
{
    public class EmployeeService : IEmployeeService
    {
        public IEmployeeRepo _employeeRepo;
        public EmployeeService(IEmployeeRepo employeeRepo)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            _employeeRepo = employeeRepo;
        }

        public MemoryStream ExportExcel()
        {
            var excel = new ExcelFile();
            var WorkSheet = excel.Worksheets.Add("Employee");
            var model = typeof(Employee).GetProperties().Select(fields => fields.Name).ToArray();
            for (int i = 0; i < model.Count(); i++)
            {
                WorkSheet.Cells[0, i].Value = model[i];
            }
            WorkSheet.Rows[0].Style.Font.Weight = ExcelFont.BoldWeight;
            var employee = _employeeRepo.GetAllEmployees().ToArray();

            for (int i = 0; i < employee.Count(); i++)
            {
                for (int j = 0; j < model.Count(); j++)
                {
                    WorkSheet.Cells[i + 1, j].Value = employee[i].GetType().GetProperty(model[j])!.GetValue(employee[i]);
                }
            }
            // excel.Save("NewFile.xls");


            MemoryStream stream = new MemoryStream();
            excel.Save(stream, SaveOptions.XlsxDefault);
            stream.Position = 0;

            return stream;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeRepo.GetAllEmployees();
        }

        public IEnumerable<Employee> InsertEmployees()
        {
            return _employeeRepo.InsertEmployees();
        }

        public bool UpdateFile(IFormFile file)
        {

            Stream stream = file.OpenReadStream();
            var excel = ExcelFile.Load(stream, LoadOptions.XlsxDefault);
            var WorkSheet = excel.Worksheets[0];

            var model = typeof(Employee).GetProperties().Select(fields => fields.Name).ToArray();
            var employee = _employeeRepo.GetAllEmployees().ToArray();

            for (int i = 0; i < employee.Count(); i++)
            {
                for (int j = 0; j < model.Count(); j++)
                {
                    string? Sheetvalue = WorkSheet.Cells[i + 1, j].Value!.ToString();
                    string? ColumnName = model[j].ToString();
                    string? DBValue = employee[i].GetType().GetProperty(ColumnName)!.GetValue(employee[i])!.ToString();
                    if (!(Sheetvalue == DBValue))
                    {
                        int Id = (int)WorkSheet.Cells[i + 1, 0].Value!;
                        // _employeeRepo.UpdateEmployee(Sheetvalue!, Id, ColumnName);
                    }
                }
            }
            //-------------------------------------------------------------------------------------------------------------------

            DataTable dataTable = new DataTable();
            for (int i = 0; i < model.Count(); i++)
            {
                dataTable.Columns.Add(model[i], typeof(Employee).GetProperties()[i].PropertyType);
            }

            for (int i = 0; i < employee.Count(); i++)
            {
                for (int j = 0; j < model.Count(); j++)
                {
                    string? Sheetvalue = WorkSheet.Cells[i + 1, j].Value!.ToString();
                    string? ColumnName = model[j].ToString();
                    string? DBValue = employee[i].GetType().GetProperty(ColumnName)!.GetValue(employee[i])!.ToString();
                    if (!(Sheetvalue == DBValue))
                    {
                        int Id = (int)WorkSheet.Cells[i + 1, 0].Value!;
                        dataTable.Rows.Add(WorkSheet.Cells[i + 1, 0].Value, WorkSheet.Cells[i + 1, 1].Value, WorkSheet.Cells[i + 1, 2].Value, WorkSheet.Cells[i + 1, 3].Value, WorkSheet.Cells[i + 1, 4].Value);
                        // _employeeRepo.UpdateEmployee(Sheetvalue!, Id, ColumnName);
                    }
                }
            }
            _employeeRepo.UDTT(dataTable);
            return true;
        }

        public bool UploadFile()
        {
            string host = "test.rebex.net";
            int port = 22;
            string username = "demo";
            string password = "password";

            var client = new SftpClient(host, port, username, password);
            // try
            // {
                // client.OperationTimeout = TimeSpan.FromMilliseconds(60000);
                client.Connect();
                Console.WriteLine("Connection established");

                // Upload a file to the remote server
                using (var fileStream = System.IO.File.OpenRead("Final.xlsx"))
                {
                    client.UploadFile(fileStream, "/remote-directory/Final.xlsx");
                }

                // Download a file from the remote server
                using (var fileStream = System.IO.File.Create("Final.xlsx"))
                {
                    client.DownloadFile("/remote-directory/Final.xlsx", fileStream);
                }

                client.Disconnect();
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine("SFTP error: " + ex.Message);
            // }


            return true;
        }
    }
}