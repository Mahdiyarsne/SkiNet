
namespace Core.Entities.OrderAggregate
{
	 public class ProductItemOrderd
	{
        public int PorductId { get; set; }

        public required string ProductName { get; set; }

        public required string PictureUrl { get; set; }
    }
}
