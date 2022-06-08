namespace Person.API.Models.Views {
	public record AddressView {
		public Guid UUID { get; set; }

		public string Street { get; set; }

		public string Complement { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public string ZipCode { get; set; }

		public AddressView(Entities.Address address) { 
			UUID = address.UUID;
			Street = address.Street;
			City = address.City;
			State = address.State;
			ZipCode = address.ZipCode;
			Complement = address.Complement;
		}
	}
}
