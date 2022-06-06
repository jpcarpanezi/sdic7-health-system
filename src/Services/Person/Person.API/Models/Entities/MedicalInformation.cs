using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Person.API.Models.Entities {
	public partial class MedicalInformation {
		[Key]
		[Column("pUUID")]
		public Guid PersonUUID { get; set; }

		[Required]
		[Column("miBloodType", TypeName = "enum('A-','A+','AB+','AB-','B+','B-','O-','O+')")]
		public string BloodType { get; set; }

		[Column("miMedicalConditions", TypeName = "text")]
		public string MedicalConditions { get; set; }

		[Column("miAllergies", TypeName = "text")]
		public string Allergies { get; set; }

		[Column("miObservations", TypeName = "text")]
		public string Observations { get; set; }


		[ForeignKey(nameof(PersonUUID))]
		[InverseProperty(nameof(Entities.Person.MedicalInformation))]
		public virtual Person Person { get; set; }
	}
}
