using Employee_Management_System.Models;
using Employee_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeServices _employeeService;
        public EmployeeController(IEmployeServices employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeAsync()
        {
            var res = new Response();
            try
            {
                var data = await _employeeService.GetEmployeeAsync();
                if (data != null)
                {
                    res.Data = data;
                    res.Success = true;
                    res.Message = "Employee data retrieved successfully.";
                    return Ok(res);
                }
                else
                {
                    res.Success = true;
                    res.Message = "Employee data not retrieved successfully.";
                    return NotFound(res);
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Error retrieving employee data: {ex.Message}";
                return BadRequest(res);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] CreateEmployeeDTO obj)
        {
            var res = new Response();
            try
            {
                if (obj != null)
                {
                    var data = await _employeeService.AddEmployeeAsync(obj);
                    if (data != null)
                    {
                        res.Data = data;
                        res.Success = true;
                        res.Message = "Employe Add successfully.";
                        return Ok(res);
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = "Some thing wrong.";
                        return BadRequest(res);
                    }
                }
                else
                {
                    res.Success = false;
                    res.Message = "Employee not Add successfully.";
                    return BadRequest(res);
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
                return BadRequest(res);
            }
        }

        [HttpGet("{employeId}")]
        public async Task<IActionResult> GetEmployeDetailsByIdAsync(string employeId)
        {
            var res = new Response();
            try
            {
                if (!string.IsNullOrWhiteSpace(employeId))
                {
                    var data = await _employeeService.GetEmployeDetailsByIdAsync(employeId);

                    if (data != null)
                    {
                        res.Data = data;
                        res.Success = true;
                        res.Message = "Employee Details get successfully.";
                        return Ok(res);
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = "Employee Details not get successfully.";
                        return NotFound(res);

                    }
                }
                else
                {
                    res.Success = false;
                    res.Message = "Employee Id null.";
                    return BadRequest(res);
                }

            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
                return BadRequest(res);
            }
        }
    }
}
