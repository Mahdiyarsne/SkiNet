using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
	public class AddressDto
	{
		[Required]
		public string Link1 { get; set; } = string.Empty;

		public string? Link2 { get; set; }

		[Required]
		public string City { get; set; } = string.Empty;

		[Required]
		public string State { get; set; } = string.Empty;

		[Required]
		public string PostalCode { get; set; } = string.Empty;

		[Required]
		public string Country { get; set; } = string.Empty;
	}
}
