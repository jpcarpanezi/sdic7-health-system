namespace Medicines.API.Models.Views {
	public record MedicalConsultationView {
		public Guid UUID { get; private set; }

		public Guid PersonUUID { get; private set; }

		public DateTime DateTime { get; private set; }

		public string Reason { get; private set; }

		public string Diagnose { get; private set; }

		public string Observations { get; private set; }

		public MedicalConsultationView(MedicalConsultation medicalConsultation) { 
			UUID = medicalConsultation.UUID;
			PersonUUID = medicalConsultation.PersonUUID;
			DateTime = medicalConsultation.DateTime;
			Reason = medicalConsultation.Reason;
			Diagnose = medicalConsultation.Diagnose;
			Observations = medicalConsultation.Observations;
		}
	}
}
