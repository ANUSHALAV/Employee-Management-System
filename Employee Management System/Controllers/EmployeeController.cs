using Employee_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        [HttpGet]
        public async Task<Response> GetEmployee()
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
                res.Message = $"Error retrieving employee data: {ex.Message}";
            }
            return res;
        }
    }
}
