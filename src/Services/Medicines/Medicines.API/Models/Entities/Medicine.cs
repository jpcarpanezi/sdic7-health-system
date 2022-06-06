using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Medicines.API.Models.Entities {
	public partial class Medicine {
		public Medicine() {
			Prescriptions = new HashSet<Prescription>();
		}

		[Key]
		[Column("mUUID")]
		public Guid UUID { get; set; }
		[Required]
		[Column("mDrugName")]
		[StringLength(255)]
		public string DrugName { get; set; }
		[Required]
		[Column("mActiveIngredient")]
		[StringLength(255)]
		public string ActiveIngredient { get; set; }
		[Required]
		[Column("mFormRoute")]
		[StringLength(255)]
		public string FormRoute { get; set; }
		[Required]
		[Column("mCompany")]
		[StringLength(255)]
		public string Company { get; set; }

		[InverseProperty(nameof(Prescription.Medicine))]
		public virtual ICollection<Prescription> Prescriptions { get; set; }
	}
}
