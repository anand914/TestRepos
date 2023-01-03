using CrudDemoDataAccessLayer.Data;
using CrudDemoDataAccessLayer.ViewModel;
using CrudDemoServicesLayer.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudApiDemo.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeService;
        public EmployeeController(IEmployee empService)
        {
            _employeeService = empService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var result = await _employeeService.GetAllemployees();
                if (result == null)
                {
                    return StatusCode(500, result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, false, new List<string> { ex.Message }, null, null));
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            try
            {
                var result = await _employeeService.ChangePassword(model);
                if (result == null)
                {
                    return StatusCode(500, result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, false, new List<string> { ex.Message }, null, null));
            }
        }

        [HttpGet]
        [ActionName("GetEmployee")]
        public async Task<IActionResult> GetemployeeById(int id)
        {
            try
            {
                var result = await _employeeService.GetEmployeeById(id);
                if (result.StatusCode == 500)
                {
                    return StatusCode(500, result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, false, new List<string> { ex.Message }, null, null));
            }
        }

        [HttpPost]
        [ActionName("AddEmployees")]
        public async Task<IActionResult> AddEmployees([FromForm] AddEmployeeVM model)
        {
            try
            {
                var result = await _employeeService.SaveEmployee(model);
                if (result.StatusCode == 500)
                {
                    return StatusCode(500, result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, false, new List<string> { ex.Message }, null, null));
            }
        }
        [HttpPost]
        [ActionName("UpdateEmployees")]
        public async Task<IActionResult> UpdateEmployees([FromForm] UpdateEmployeeVM model)
        {
            try
            {
                var result = await _employeeService.UpdateemployeeInfo(model);
                if (result.StatusCode == 500)
                {
                    return StatusCode(500, result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, false, new List<string> { ex.Message }, null, null));
            }
        }

        [HttpPost]
        [ActionName("DeleteEmployees")]
        public async Task<IActionResult> DeleteEmployees([FromForm] int id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployee(id);
                if (result.StatusCode == 500)
                {
                    return StatusCode(500, result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, false, new List<string> { ex.Message }, null, null));
            }
        }
        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> EmployeeLogin([FromForm] LoginVM model)
        {
            try
            {
                var result = await _employeeService.Login(model);
                if (result.StatusCode == 500)
                {
                    return StatusCode(500, result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, false, new List<string> { ex.Message }, null, null));
            }
            //}
        }

        //[HttpPost]
        //public IActionResult SendEmail(EmailModelVM request)
        //{
        //    _employeeService.SendEmail(request);
        //    return Ok();
        //}
    }
}
