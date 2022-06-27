namespace Person.API.Models.Inserts {
	public record InsertAddress {
		public Guid PersonUUID { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(255)]
		public string Street { get; set; }

		[MaxLength(255)]
		public string Complement { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(100)]
		public string City { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(64)]
		public string State { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(20)]
		public string ZipCode { get; set; }
	}
}
