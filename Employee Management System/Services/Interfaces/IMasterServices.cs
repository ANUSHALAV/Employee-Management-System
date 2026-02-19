using Employee_Management_System.Models;
namespace Employee_Management_System.Services.Interfaces
{
    public interface IMasterServices
    {
        public Task<List<Departments>> GetDepartmentAsync();

        public Task<List<Roles>> GetRolesAsync();

        public Task<List<Grades>> GetGradesAsync();

    }
}
