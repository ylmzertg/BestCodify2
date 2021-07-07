using BestCodify.Common;
using BestCodify.Models;
using BestCodify2_Client.Service.IServices;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
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

        public async Task<Result<CourseOrderInfoDto>> SaveCourseOrderInfo(CourseOrderInfoDto model)
        {
            var content = JsonConvert.SerializeObject(model);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/courseOrder/create", bodyContent);
            string res = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var a =   response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<CourseOrderInfoDto>(a);
                return new Result<CourseOrderInfoDto>(true, ResultConstant.RecordFound, result);
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<CourseOrderInfoDto>(contentTemp);
                return new Result<CourseOrderInfoDto>(false, ResultConstant.RecordNotFound);
            }
        }
    }
}
