using BestCodify.Common;
using BestCodify.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestCodify2_Client.Service.IServices
{
    public interface ICourseService
    {
        public Task<Result<IEnumerable<CourseDto>>> GetAllCourse();
        public Task<Result<CourseDto>> GetCourse(int courseId);
    }
}
