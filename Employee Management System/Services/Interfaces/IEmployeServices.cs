using Employee_Management_System.Models;

namespace Employee_Management_System.Services.Interfaces
{
    public interface IEmployeServices
    {
        public Task<List<Employees>> GetEmployeeAsync();

        public Task<Employees> AddEmployeeAsync(Employees obj);

        public Task<EmployeeDetailDto> GetEmployeDetailsByIdAsync(string employeId);
    }
}
