using Employee_Management_System.Models;

namespace Employee_Management_System.Services.Interfaces
{
    public interface IEmployeServices
    {
        public Task<List<Employees>> GetEmploye();

        public Task<Employees> AddEmployee(Employees obj);
    }
}
