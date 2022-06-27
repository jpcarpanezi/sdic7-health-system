namespace Person.API.Models.Inserts {
	public record InsertMedicalInformation {
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
