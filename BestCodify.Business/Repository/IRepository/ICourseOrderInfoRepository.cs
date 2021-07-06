using BestCodify.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestCodify.Business.Repository.IRepository
{
    public interface ICourseOrderInfoRepository
    {
        public Task<CourseOrderInfoDto> Create(CourseOrderInfoDto details);
        public Task<CourseOrderInfoDto> PaymentSuccessful(CourseOrderInfoDto details);
        public Task<CourseOrderInfoDto> GetCourseOrderInfo(CourseOrderInfoDto details);
        public Task<IEnumerable<CourseOrderInfoDto>> GetAllCourseOrderInfos(CourseOrderInfoDto details);
        public Task<bool> UpdateCourseOrderStatus(int courseOrderId, string status);

    }
}
