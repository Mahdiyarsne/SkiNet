using Core.Entities;

namespace Core.Interfaces
{
	public interface IPaymentService
	{
		Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId);

		Task<string> RefoundPayment(string paymentIntentId);
	}
}
