namespace Person.API.Models.Updates {
	public record UpdateAddress {
		[Required]
		public Guid UUID { get; set; }

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
		[StringLength(20)]
		public string ZipCode { get; set; }
	}
}
