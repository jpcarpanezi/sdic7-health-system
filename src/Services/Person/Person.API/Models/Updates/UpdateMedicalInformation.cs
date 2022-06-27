namespace Person.API.Models.Updates {
	public record UpdateMedicalInformation {
		public Guid PersonUUID { get; set; }

		[Required(AllowEmptyStrings = false)]
		public string BloodType { get; set; }

		[MaxLength(250)]
		public string MedicalConditions { get; set; }

		[MaxLength(250)]
		public string Allergies { get; set; }

		[MaxLength(500)]
		public string Observations { get; set; }
	}
}
