using API.DTOs;
using Core.Entities;

namespace API.Extensions
{
	public static class AddressMappingExtensions
	{
		public static AddressDto? ToDto(this Address? address)
		{
			if (address == null)  return null;

			return new AddressDto
			{
				Link1 = address.Link1,
				Link2 = address.Link2,
				City = address.City,
				State = address.State,
				PostalCode = address.PostalCode,
				Country = address.Country,
			};
		}


		public static Address ToEntity(this AddressDto addressDto)
		{
			if (addressDto == null) throw new ArgumentNullException(nameof(addressDto));

			return new Address
			{
				Link1 = addressDto.Link1,
				Link2 = addressDto.Link2,
				City = addressDto.City,
				State = addressDto.State,
				PostalCode = addressDto.PostalCode,
				Country = addressDto.Country,
			};
		}


		public static void UpdateFromDto(this Address address, AddressDto addressDto)
		{
			if (addressDto == null) throw new ArgumentNullException(nameof(addressDto));
			if (address == null) throw new ArgumentNullException(nameof(address));

			address.Link1 = addressDto.Link1;
			address.Link2 = addressDto.Link2;
			address.City = addressDto.City;
			address.State = addressDto.State;
			address.PostalCode = addressDto.PostalCode;
			address.Country = addressDto.Country;

		}
	}
}