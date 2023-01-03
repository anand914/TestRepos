using CrudDemoDataAccessLayer.ViewModel;
using CrudDemoServicesLayer.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrudApiDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGender _genSrvc;

        public GenderController(IGender genSrvc)
        {
            _genSrvc = genSrvc;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenders()
        {
            try
            {
                var result = await _genSrvc.GetAllGenderInfo();
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
        public async Task<IActionResult> AddOrUpdateGenderInfo(GenderVM model)
        {
            try
            {
                var result = await _genSrvc.AddOrUpdateGender(model);
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
