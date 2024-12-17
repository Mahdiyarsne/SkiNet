
namespace Core.Entities.OrderAggregate
{
	public class ShippingAddress
	{
		public required string Name { get; set; }

		public required string Link1 { get; set; }

		public string? Link2 { get; set; }

		public required string City { get; set; }

		public required string State { get; set; }

		public required string PostalCode { get; set; }

		public required string Country { get; set; }
	}
}
