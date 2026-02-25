using Employee_Management_System.Models;

namespace Employee_Management_System.Services.Interfaces
{
    public interface IEmployeServices
    {
        public Task<List<ResponseEmployeeDTO>> GetEmployeeAsync();

        public Task<CreateEmployeeDTO> AddEmployeeAsync(CreateEmployeeDTO obj);

        public Task<EmployeeDetailDto> GetEmployeDetailsByIdAsync(string employeId);
    }
}
