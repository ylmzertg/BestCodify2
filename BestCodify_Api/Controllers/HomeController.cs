using BestCodify.Business.Repository.IRepository;
using BestCodify.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BestCodify_Api.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public HomeController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var allCourse = await _courseRepository.GetAllCourse();
            var data = allCourse.Data;
            return Ok(data);
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourse(int? courseId)
        {
            if (courseId == null)
            {
                return BadRequest(new Result<IActionResult>(false, ResultConstant.IdNotNull));
            }
            var allCourse = await _courseRepository.GetCourse((int)courseId);
            if (allCourse != null)
                return Ok(allCourse);
            else
                return BadRequest(new Result<IActionResult>(false, ResultConstant.RecordFound));
        }
    }
}
