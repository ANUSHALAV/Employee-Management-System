using Employee_Management_System.Models;
namespace Employee_Management_System.Services.Interfaces
{
    public interface IMasterServices
    {
        public Task<List<Departments>> GetDepartment();

        public Task<List<Roles>> GetRoles();

    }
}
