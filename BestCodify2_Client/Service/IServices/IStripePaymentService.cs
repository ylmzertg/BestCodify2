using BestCodify.Common;
using BestCodify.Models;
using System.Threading.Tasks;

namespace BestCodify2_Client.Service.IServices
{
    public interface IStripePaymentService
    {
        public Task<Result<StripeSuccessfullModel>> CheckOut(StriperPaymentDto model);
    }
}
