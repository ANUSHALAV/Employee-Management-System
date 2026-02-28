using Employee_Management_System.Models;
using Employee_Management_System.Services.Implementations;
using Employee_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasterController : ControllerBase
    {
        private readonly IMasterServices _masterServices;
        private readonly ILogger<MasterController> _logger; 
        public MasterController(IMasterServices masterServices, ILogger<MasterController> logger)
        {
            this._masterServices = masterServices;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("departments")]
        public async Task<Response> GetDepartmentAsync()
        {
            var res = new Response();
            try
            {
                var data = await _masterServices.GetDepartmentAsync();
                if (data != null)
                {
                    res.Data = data;
                    res.Success = true;
                    res.Message = "Department data retrieved successfully.";
                    _logger.LogInformation("Department data retrieved successfully.");
                }
                else
                {
                    res.Success = false;
                    res.Message = "Department data not retrieved successfully.";
                    _logger.LogWarning("Department data not retrieved successfully.");
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Error retrieving department data: {ex.Message}";
                _logger.LogError(ex, "Error retrieving department data.");
            }
            return res;

        }

        [Authorize]
        [HttpGet("roles")]
        public async Task<Response> GetRolesAsync()
        {
            var res = new Response();
            try
            {
                var data = await _masterServices.GetRolesAsync();
                if (data != null)
                {
                    res.Data = data;
                    res.Success = true;
                    res.Message = "Roles Data retrieved successfully.";
                    _logger.LogInformation("Roles data retrieved successfully.");
                }
                else
                {
                    res.Success = false;
                    res.Message = "Roles Data not retrieves successfully.";
                    _logger.LogWarning("Roles data not retrieved successfully.");

                }
            }
            catch(Exception ex)
            {
                res.Success = false;
                res.Message = $"Error retrieving Roles data: {ex.Message}";
                _logger.LogError(ex, "Error retrieving Roles data.");
            }
            return res;
        }

        [Authorize]
        [HttpGet("grades")]
        public async Task<Response> GetGradesAsync()
        {
            var res = new Response();
            try
            {
                var data = await _masterServices.GetGradesAsync();
                res.Data = data;
                res.Success = true;
                res.Message = "Grade Data not retrieves successfully.";
                _logger.LogInformation("Grade data retrieved successfully.");
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
                _logger.LogError(ex, "Error retrieving Grade data.");
            }
            return res;
        }
    }
}
