using BestCodify.Common;
using BestCodify.Models;
using System.Threading.Tasks;

namespace BestCodify2_Client.Service.IServices
{
    public interface ICourseOrderInfoService
    {
        public Task<Result<CourseOrderInfoDto>> SaveCourseOrderInfo();
        public Task<Result<CourseOrderInfoDto>> PaymentSuccessful(int courseId);
    }
}
