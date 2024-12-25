using API.DTOs;
using API.Extensions;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Authorize(Roles ="Admin")]

	public class AdminController(IUnitOfWork unit ,IPaymentService paymentService) : BaseApiController
	{
		[HttpGet("orders")]
		public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrders([FromQuery]OrderSpecParams specParams)
		{
			var spec = new OrderSpecification(specParams);

			return await CreatePage(unit.Repository<Order>(), spec,
				specParams.PageIndex, 
				specParams.PageSize, o => o.ToDto());
		}

		[HttpGet("orders/{id:int}")]
		public async Task<ActionResult<OrderDto>> GetOrderById(int id)
		{
			var spec = new OrderSpecification(id);

			var order = await unit.Repository<Order>().GetEntityWithSpec(spec);

			if (order == null) return BadRequest("No order with such an id");

			return order.ToDto();

		}

		[HttpGet("orders/refund/{id:int}")]

		public async Task<ActionResult<OrderDto>> Refund(int id)
		{
			var spec = new OrderSpecification(id);
			var order = await unit.Repository<Order>().GetEntityWithSpec(spec);

			if(order == null) return BadRequest("No order with such an id");

			if (order.Status == OrderStatus.Pending)
				return BadRequest("Payment not recived for this order");

			var result = await paymentService.RefoundPayment(order.PaymentIntentId);

			if(result == "succeeded")
			{
				order.Status = OrderStatus.Refunded;

				await unit.Complete();

				return order.ToDto();
			}
			return BadRequest("Problem refunding with order");
		}
	}
}
