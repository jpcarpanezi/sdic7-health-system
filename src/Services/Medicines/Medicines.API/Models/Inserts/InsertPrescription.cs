using System.ComponentModel.DataAnnotations;

namespace Medicines.API.Models.Inserts {
	public class InsertPrescription {
		public Guid MedicalConsultationUUID { get; set; }

		public Guid MedicineUUID { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(64)]
		public string Dosage { get; set; }
	}
}
