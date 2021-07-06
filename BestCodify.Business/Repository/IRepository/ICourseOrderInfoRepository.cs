using BestCodify.Common;
using BestCodify.Models;
using System.Threading.Tasks;

namespace BestCodify.Business.Repository.IRepository
{
    public interface ICourseOrderInfoRepository
    {
        public Task<Result<CourseOrderInfoDto>> Create(CourseOrderInfoDto details);
        public Task<Result<CourseOrderInfoDto>> PaymentSuccessful(CourseOrderInfoDto details);
        public Task<Result<CourseOrderInfoDto>> GetCourseOrderInfo(int courseId);
        public Task<Result<bool>> UpdateCourseOrderStatus(int courseOrderId, string status);

    }
}
