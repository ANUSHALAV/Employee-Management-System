using Employee_Management_System.Services.Interfaces;
using Employee_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    [ApiController]
    public class MasterController : Controller
    {
        private readonly IMasterServices masterServices;
        public MasterController(IMasterServices _masterServices)
        {
            this.masterServices = _masterServices;
        }

        [HttpGet]
        [Route("GetDepartmentAsync")]
        public async Task<Response> GetDepartmentAsync()
        {
            var res = new Response();
            try
            {
                var Data = await masterServices.GetDepartmentAsync();
                if (Data != null)
                {
                    res.Data = Data;
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
                var Data = await masterServices.GetRolesAsync();
                if (Data != null)
                {
                    res.Data = Data;
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
                var data = await masterServices.GetGradesAsync();
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
