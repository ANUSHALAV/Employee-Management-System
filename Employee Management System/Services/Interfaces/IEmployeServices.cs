using Employee_Management_System.Models;

namespace Employee_Management_System.Services.Interfaces
{
    public interface IEmployeServices
    {
        public Task<ResponseLoginDTO> Login(LoginDTO obj);
        public Task<List<ResponseEmployeeDTO>> GetEmployeeAsync();

        public Task<CreateEmployeeDTO> AddEmployeeAsync(CreateEmployeeDTO obj);

        public Task<ResponseEmployeeDTO> GetEmployeDetailsByIdAsync(string employeId);
    }
}
