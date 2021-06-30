using BestCodify.Common;
using BestCodify.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestCodify.Business.Repository.IRepository
{
    public interface ICourseImageRepository
    {
        public Task<Result<int>> CreateCourseImage(CourseImageDto model);
        public Task<Result<int>> DeleteCourseImageByImageId(int imageId);
        public Task<Result<int>> DeleteCourseImageByCourseId(int courseId);
        public Task<IEnumerable<CourseImageDto>> GetCourseImages(int courseId);
    }
}
