using Employee_Management_System.Services.Interfaces;
using Employee_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MasterController : Controller
    {
        private readonly IMasterServices masterServices;
        public MasterController(IMasterServices _masterServices)
        {
            this.masterServices = _masterServices;
        }

        [HttpGet]
        [Route("GetDeparment")]
        public async Task<Response> GetDepartment()
        {
            var res = new Response();
            try
            {
                var Data = await masterServices.GetDepartment();
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
    }
}
