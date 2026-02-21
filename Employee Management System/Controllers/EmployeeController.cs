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
        [Route("GetEmployeeAsync")]
        public async Task<Response> GetEmployeeAsync()
        {
            var res = new Response();
            try
            {
                var data = await employeeService.GetEmployeeAsync();
                if (data != null)
                {
                    res.Data = data;
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
        [Route("AddEmployeeAsync")]
        public async Task<Response> AddEmployeeAsync([FromBody] Employees obj)
        {
            var res = new Response();
            try
            {
                if (obj != null)
                {
                    var data = await employeeService.AddEmployeeAsync(obj);
                    res.Data = data;
                    res.Success = true;
                    res.Message = "Employe Add successfully.";
                }
                else
                {
                    res.Success = false;
                    res.Message = "Employee not Add successfully.";
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpGet]
        [Route("GetEmployeDetailsByIdAsync")]
        public async Task<Response> GetEmployeDetailsByIdAsync(string employeId)
        {
            var res = new Response();
            try
            {
                var data = await employeeService.GetEmployeDetailsByIdAsync(employeId);

                if (data != null)
                {
                    res.Data = data;
                    res.Success = true;
                    res.Message = "Employee Details get successfully.";
                }
                else
                {
                    res.Success = true;
                    res.Message = "Employee Details not get successfully.";
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = "Employee Details not get successfully.";
            }
            return res;
        }
    }
}
