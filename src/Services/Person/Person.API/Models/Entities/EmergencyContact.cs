using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Person.API.Models.Entities {
	[Index(nameof(PersonUUID), Name = "fk_EmergencyContacts_Person")]
	public partial class EmergencyContact {
		[Key]
		[Column("ecUUID")]
		public Guid UUID { get; set; }

		[Column("pUUID")]
		public Guid PersonUUID { get; set; }

		[Required]
		[Column("ecName")]
		[StringLength(255)]
		public string Name { get; set; }

		[Required]
		[Column("ecPhone")]
		[StringLength(16)]
		public string Phone { get; set; }

		[Required]
		[Column("ecKinship")]
		[StringLength(255)]
		public string Kinship { get; set; }


		[ForeignKey(nameof(PersonUUID))]
		[InverseProperty(nameof(Entities.Person.EmergencyContacts))]
		public virtual Person Person { get; set; }
	}
}
