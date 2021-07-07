using BestCodify.Common;
using BestCodify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestCodify_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Create(StriperPaymentDto model)
        {
            try
            {
                var domain = _configuration.GetValue<string>("BestCodify_Client_URL");

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                        "card"
                    },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount=12* 100,//convert to cents
                                Currency="usd",
                                ProductData= new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = model.ProductName
                                }
                            },
                            Quantity=1
                        }
                    },
                    Mode = "payment",
                    SuccessUrl = domain + "/success-payment?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = domain + model.ReturnUrl
                };

                var service = new SessionService();
                Session session = await service.CreateAsync(options);
                //StripeSuccessfullModel returnModel = new StripeSuccessfullModel
                //{
                //    sessionId = session.Id
                //};
                return Ok(new StripeSuccessfullModel()
                {
                        sessionId = session.Id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new StripeSuccessfullModel()
                {
                    ErrorMessage = ex.Message.ToString()
                });
            }
        }
    }
}
