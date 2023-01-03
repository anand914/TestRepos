using CrudDemoDataAccessLayer.ViewModel;
using CrudDemoServicesLayer.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrudApiDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment _depSrvc;
        public DepartmentController(IDepartment depSrvc)
        {
            _depSrvc = depSrvc;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var result = await _depSrvc.GetDepartments();
                if (result.StatusCode == 500)
                {
                    return StatusCode(500, result);
                }
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [ActionName("GetDepartment")]
        public async Task<IActionResult> GetDepartmentbyId(int id)
        {
            try
            {
                var result = await _depSrvc.GetDepartmentbyId(id);
                if (result.StatusCode == 500)
                {
                    return StatusCode(500, result);
                }
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [ActionName("AddOrUpdateDepartment")]
        public async Task<IActionResult> AddOrUpdateDepartmentbyId([FromForm] DepartmentVM model)
        {
            try
            {
                var result = await _depSrvc.AddOrUpdateDepartment(model);
                if (result.StatusCode == 500)
                {
                    return StatusCode(500, result);
                }
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmpByDepId(int depId)
        {
            try
            {
                var result = await _depSrvc.GetAllEmployeebydepId(depId);
                if (result.StatusCode == 500)
                {
                    return StatusCode(500, result);
                }
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
