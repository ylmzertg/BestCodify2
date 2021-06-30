using BestCodify.Common;
using BestCodify.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestCodify.Business.Repository.IRepository
{
    public interface ICourseRepository
    {
        public Task<Result<CourseDto>> CreateCourse(CourseDto courseDto);
        public Task<Result<CourseDto>> UpdateCourse(int courseId, CourseDto courseDto);
        public Task<Result<CourseDto>> GetCourse(int courseId);
        public Task<Result<int>> DeleteCourse(int courseId);

        public Task<Result<IEnumerable<CourseDto>>> GetAllCourse();

        //Bellkı ıhtıyac olmıyabılır
        public Task<Result<CourseDto>> IsCourseUnique(string name, int courseId = 0);

    }
}
