using System.ComponentModel.DataAnnotations;

namespace Medicines.API.Models.Updates {
	public class UpdatePrescription {
		[Required]
		public Guid UUID { get; set; }

		public Guid MedicalConsultationUUID { get; set; }

		public Guid MedicineUUID { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(64)]
		public string Dosage { get; set; }
	}
}
