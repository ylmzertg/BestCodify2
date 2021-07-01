using BestCodify.Common;
using BestCodify.Models;
using BestCodify2_Client.Service.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BestCodify2_Client.Service
{
    public class CourseService : ICourseService
    {
        //TODO:Client aracılıgı ıle api tarafına ulasıp datamızı alıcaz.

        private readonly HttpClient _httpClient;

        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<IEnumerable<CourseDto>>> GetAllCourse()
        {
            var response = await _httpClient.GetAsync($"api/Home");
            var content = await response.Content.ReadAsStringAsync();
            var courses = JsonConvert.DeserializeObject<IEnumerable<CourseDto>>(content);
            return new Result<IEnumerable<CourseDto>>(true, ResultConstant.RecordFound, courses);
        }

        public Task<Result<CourseDto>> GetCourse(int courseId)
        {
            throw new NotImplementedException();
        }
    }
}
