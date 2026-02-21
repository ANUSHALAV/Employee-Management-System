using Employee_Management_System.Models;
using Employee_Management_System.Services.Implementations;
using Employee_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    [ApiController]
    public class MasterController : Controller
    {
        private readonly IMasterServices _masterServices;
        public MasterController(IMasterServices masterServices)
        {
            this._masterServices = _masterServices;
        }

        [HttpGet]
        [Route("GetDepartmentAsync")]
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
                }
                else
                {
                    res.Success = false;
                    res.Message = "Department data not retrieved successfully.";
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Error retrieving department data: {ex.Message}";
            }
            return res;

        }

        [HttpGet]
        [Route("GetRolesAsync")]
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
                }
                else
                {
                    res.Success = false;
                    res.Message = "Roles Data not retrieves successfully.";

                }
            }
            catch(Exception ex)
            {
                res.Success = false;
                res.Message = $"Error retrieving Roles data: {ex.Message}";
            }
            return res;
        }

        [HttpGet]
        [Route("GetGradesAsync")]
        public async Task<Response> GetGradesAsync()
        {
            var res = new Response();
            try
            {
                var data = await _masterServices.GetGradesAsync();
                res.Data = data;
                res.Success = true;
                res.Message = "Grade Data not retrieves successfully.";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
            }
            return res;
        }
    }
}
