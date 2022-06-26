namespace Person.API.Models.Inserts {
	public record InsertEmergencyContact {
		[Required]
		public Guid UUID { get; set; }

		[Required]
		public Guid PersonUUID { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(255)]
		public string Name { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(16)]
		public string Phone { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(255)]
		public string Kinship { get; set; }
	}
}
