using Employee_Management_System.Models;
using Employee_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeServices employeeService;
        public EmployeeController(IEmployeServices _employeeService)
        {
            this.employeeService = _employeeService;
        }

        [HttpGet]
        public async Task<Response> GetEmployee()
        {
            var res = new Response();
            try
            {
                var Data = await employeeService.GetEmploye();
                if (Data != null)
                {
                    res.Data = Data;
                    res.Success = true;
                    res.Message = "Employee data retrieved successfully.";
                }
                else
                {
                    res.Success = true;
                    res.Message = "Employee data not retrieved successfully.";
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Error retrieving employee data: {ex.Message}";
            }
            return res;
        }
    }
}
