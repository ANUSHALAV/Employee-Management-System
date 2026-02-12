using Employee_Management_System.Models;
using Employee_Management_System.Services.Interfaces;

namespace Employee_Management_System.Services.Implementations
{
    public class EmployeServices : IEmployeServices
    {

        public async Task<Response> GetEmploye()
        {
            var res = new Response();
            try
            {   
                res.Success = true;
                res.Message = "Employee data retrieved successfully.";

            }
            catch(Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
            }
            return res;
        }
    }
}
