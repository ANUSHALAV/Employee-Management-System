using Employee_Management_System.Models;
using Employee_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeServices employeeService;
        public EmployeeController(IEmployeServices _employeeService)
        {
            this.employeeService = _employeeService;
        }

        [HttpGet]
        [Route("GetEmployee")]
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

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<Response> AddEmployee([FromBody] Employees obj)
        {
            var res = new Response();
            try
            {
                if (obj != null)
                {
                    var Data = await employeeService.AddEmployee(obj);
                    res.Data = Data;
                    res.Success = true;
                    res.Message = "Employe Add successfully.";
                }
                else
                {
                    res.Success = false;
                    res.Message = "Employee not Add successfully.";
                }
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
