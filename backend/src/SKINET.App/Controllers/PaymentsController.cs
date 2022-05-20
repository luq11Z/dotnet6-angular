using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SKINET.App.Errors;
using SKINET.Business.Interfaces.IServices;
using SKINET.Business.Models;
using Stripe;
using Order = SKINET.Business.Models.OrderAggregate.Order;

namespace SKINET.App.Controllers
{
    public class PaymentsController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;
        private const string WhSecret = "";

        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("{shoppingCartId}")]
        public async Task<ActionResult<ShoppingCart>> CreateOrUpdatePaymentIntent(string shoppingCartId)
        {
            var shoppingCart = await _paymentService.CreateOrUpdatePaymentIntent(shoppingCartId);

            if (shoppingCart == null)
            {
                return BadRequest(new ApiResponse(404, "There was a problem with your shopping cart"));
            }

            return Ok(shoppingCart);
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhok()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret);

            PaymentIntent intent;
            Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded: ", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Order updated to payment received: ", order.Id);
                    break;

                case "payment_intent.payment_failed": 
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Failed: ", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _logger.LogInformation("Payment Failed: ", order.Id);
                    break;
            }

            return new EmptyResult();
        }
    }
}
