using System.ComponentModel.DataAnnotations;

namespace Medicines.API.Models.Updates {
	public class UpdatePrescription {
		public Guid UUID { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(64)]
		public string Dosage { get; set; }
	}
}
