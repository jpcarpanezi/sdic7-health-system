namespace Medicines.API.Models.Views {
	public record PrescriptionView {
		public Guid UUID { get; set; }

		public Guid MedicalConsultationUUID { get; set; }

		public Guid MedicineUUID { get; set; }

		public string Dosage { get; set; }

		public PrescriptionView(Entities.Prescription prescription) {
			UUID = prescription.UUID;
			MedicalConsultationUUID = prescription.MedicalConsultationUUID;
			MedicineUUID = prescription.MedicineUUID;
			Dosage = prescription.Dosage;
		}
	}
}
