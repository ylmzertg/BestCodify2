using BestCodify.Business.Repository.IRepository;
using BestCodify.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BestCodify_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CourseOrderController : Controller
    {
        private readonly ICourseOrderInfoRepository _courseOrderInfoRepository;

        public CourseOrderController(ICourseOrderInfoRepository courseOrderInfoRepository)
        {
            _courseOrderInfoRepository = courseOrderInfoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseOrderInfoDto model)
        {
            //TODO:Bılerek burada resut olmadan gerıye data don
            if (ModelState.IsValid)
            {
                var result = await _courseOrderInfoRepository.Create(model);
                return Ok(result.Data);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
