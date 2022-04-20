using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SKINET.App.Dtos;
using SKINET.App.Errors;
using SKINET.App.Extensions;
using SKINET.Business.Interfaces;
using SKINET.Business.Models.OrderAggregate;

namespace SKINET.App.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.shoppingCartId, address);

            if (order == null)
            {
                return BadRequest(new ApiResponse(400, "Problem creating order"));
            }

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var orders = await _orderService.GetAllOrdersForUserAsync(email);

            return Ok(_mapper.Map<List<Order>, List<OrderToReturnDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<List<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
    }
}
