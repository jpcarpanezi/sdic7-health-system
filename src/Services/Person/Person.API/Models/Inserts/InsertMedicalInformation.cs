namespace Person.API.Models.Inserts {
	public record InsertMedicalInformation {
		[Required]
		public Guid PersonUUID { get; set; }

		[Required]
		public string BloodType { get; set; }

		public string MedicalConditions { get; set; }

		public string Allergies { get; set; }

		public string Observations { get; set; }
	}
}
