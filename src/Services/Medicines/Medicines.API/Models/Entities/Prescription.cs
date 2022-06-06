using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Medicines.API.Models.Entities {
	[Index(nameof(MedicalConsultationUUID), Name = "fk_Prescriptions_MedicalConsultation")]
	[Index(nameof(MedicineUUID), Name = "fk_Prescriptions_Medicines")]
	public partial class Prescription {
		[Key]
		[Column("pUUID")]
		public Guid UUID { get; set; }

		[Column("mcUUID")]
		public Guid MedicalConsultationUUID { get; set; }

		[Column("mUUID")]
		public Guid MedicineUUID { get; set; }

		[Required]
		[Column("pDosage")]
		[StringLength(64)]
		public string Dosage { get; set; }


		[ForeignKey(nameof(MedicineUUID))]
		[InverseProperty(nameof(Entities.Medicine.Prescriptions))]
		public virtual Medicine Medicine { get; set; }

		[ForeignKey(nameof(MedicalConsultationUUID))]
		[InverseProperty(nameof(Entities.MedicalConsultation.Prescriptions))]
		public virtual MedicalConsultation MedicalConsultation { get; set; }
	}
}
