namespace Person.API.Models.Views {
	public record MedicalInformationView {
		public Guid PersonUUID { get; set; }

		public string BloodType { get; set; }

		public string MedicalConditions { get; set; }

		public string Allergies { get; set; }

		public string Observations { get; set; }

		public MedicalInformationView(Entities.MedicalInformation medicalInformation) {
			PersonUUID = medicalInformation.PersonUUID;
			BloodType = medicalInformation.BloodType;
			MedicalConditions = medicalInformation.MedicalConditions;
			Allergies = medicalInformation.Allergies;
			Observations = medicalInformation.Observations;
		}
	}
}
