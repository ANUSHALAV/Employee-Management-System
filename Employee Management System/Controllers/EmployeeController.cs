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
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeServices employeeService, ILogger<EmployeeController> logger)
        {
            this._employeeService = employeeService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO obj)
        {
            var res = new Response();
            try
            {
                if (obj != null)
                {
                    var data = await _employeeService.Login(obj);
                    if (data != null)
                    {
                        res.Data = data;
                        res.Success = true;
                        res.Message = "Login successfully.";
                        _logger.LogInformation("Login successful for EmployeeId: {EmployeeId}", obj.EmployeeId);
                        return Ok(res);
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = "Login not successfully.";
                        _logger.LogWarning("Login failed for EmployeeId: {EmployeeId}", obj.EmployeeId);
                        return NotFound(res);
                    }
                }
                else
                {
                    res.Success = false;
                    res.Message = "Login not successfully.";
                    _logger.LogWarning("Login failed due to null LoginDTO object.");
                    return BadRequest(res);
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
                _logger.LogError(ex, "Error in Login for EmployeeId: {EmployeeId}", obj?.EmployeeId);
                return BadRequest(res);
            }
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
                    _logger.LogInformation("Employee data retrieved successfully.");
                    return Ok(res);
                }
                else
                {
                    res.Success = true;
                    res.Message = "Employee data not retrieved successfully.";
                    _logger.LogWarning("Employee data not retrieved successfully.");
                    return NotFound(res);
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Error retrieving employee data: {ex.Message}";
                _logger.LogError(ex, "Error in GetEmployeeAsync");
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
                        _logger.LogInformation("Employee added successfully with ID: {EmployeeId}", data.EmployeeId);
                        return Ok(res);
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = "Some thing wrong.";
                        _logger.LogWarning("Employee not added successfully for EmployeeId: {EmployeeId}", obj.EmployeeId);
                        return BadRequest(res);
                    }
                }
                else
                {
                    res.Success = false;
                    res.Message = "Employee not Add successfully.";
                    _logger.LogWarning("Employee object is null in AddEmployeeAsync.");
                    return BadRequest(res);
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
                _logger.LogError(ex, "Error in AddEmployeeAsync for EmployeeId: {EmployeeId}", obj?.EmployeeId);
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
                        _logger.LogInformation("Employee details retrieved successfully for EmployeeId: {EmployeeId}", employeId);
                        return Ok(res);
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = "Employee Details not get successfully.";
                        _logger.LogWarning("Employee details not retrieved successfully for EmployeeId: {EmployeeId}", employeId);
                        return NotFound(res);

                    }
                }
                else
                {
                    res.Success = false;
                    res.Message = "Employee Id null.";
                    _logger.LogWarning("Employee ID is null or whitespace in GetEmployeDetailsByIdAsync.");
                    return BadRequest(res);
                }

            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
                _logger.LogError(ex, "Error in GetEmployeDetailsByIdAsync for EmployeeId: {EmployeeId}", employeId);
                return BadRequest(res);
            }
        }
    }
}
