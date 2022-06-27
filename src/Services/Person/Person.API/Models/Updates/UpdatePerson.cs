namespace Person.API.Models.Updates {
	public record UpdatePerson {
		public Guid UUID { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(255)]
		public string Name { get; set; }

		[Required]
		[MaxLength(15)]
		[DataType(DataType.PhoneNumber)]
		[RegularExpression("^\\(?[1-9]{2}\\)? ?(?:[2-8]|9[1-9])[0-9]{3}\\-?[0-9]{4}$")]
		public string Phone { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime BirthDate { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(120)]
		[EmailAddress]
		public string Email { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(100)]
		public string BirthCity { get; set; }
	}
}
