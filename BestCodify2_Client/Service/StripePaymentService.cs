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
    public class StripePaymentService : IStripePaymentService
    {
        //TODO:Client aracılıgı ıle api tarafına ulasıp datamızı alıcaz.

        private readonly HttpClient _httpClient;

        public StripePaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<StripeSuccessfullModel>> CheckOut(StriperPaymentDto model)
        {
            var content = JsonConvert.SerializeObject(model);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Payment/create", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content;
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<StripeSuccessfullModel>(contentTemp);
                return new Result<StripeSuccessfullModel>(true,ResultConstant.RecordFound,result);
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<StripeSuccessfullModel>(contentTemp);
                return new Result<StripeSuccessfullModel>(false, ResultConstant.RecordNotFound);
            }
        }
    }
}
