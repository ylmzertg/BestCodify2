using BestCodify.Common;
using BestCodify.Models;
using BestCodify2_Client.Service.IServices;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BestCodify2_Client.Service
{
    public class CourseOrderInfoService : ICourseOrderInfoService
    {
        //TODO:Client aracılıgı ıle api tarafına ulasıp datamızı alıcaz.

        private readonly HttpClient _httpClient;

        public CourseOrderInfoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<Result<CourseOrderInfoDto>> PaymentSuccessful(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<CourseOrderInfoDto>> SaveCourseOrderInfo()
        {
            throw new NotImplementedException();
        }
    }
}
