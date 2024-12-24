
namespace Core.Entities.OrderAggregate
{
	 public enum OrderStatus
	{
		Pending,
	    
		PaymentRececived,

		PaymentFailed,

		PaymentMismatch
	}
}
