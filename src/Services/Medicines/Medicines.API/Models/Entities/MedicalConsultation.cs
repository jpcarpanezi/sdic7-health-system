using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medicines.API.Models.Entities {
	[Table("MedicalConsultation")]
	public partial class MedicalConsultation {
		public MedicalConsultation() {
			Prescriptions = new HashSet<Prescription>();
		}

		[Key]
		[Column("mcUUID")]
		public Guid UUID { get; set; }

		[Column("pUUID")]
		public Guid PersonUUID { get; set; }

		[Column("mcDateTime", TypeName = "timestamp")]
		public DateTime DateTime { get; set; }

		[Required]
		[Column("mcReason", TypeName = "text")]
		public string Reason { get; set; }

		[Required]
		[Column("mcDiagnose", TypeName = "text")]
		public string Diagnose { get; set; }

		[Required]
		[Column("mcObservations", TypeName = "text")]
		public string Observations { get; set; }


		[InverseProperty(nameof(Prescription.MedicalConsultation))]
		public virtual ICollection<Prescription> Prescriptions { get; set; }
	}
}
