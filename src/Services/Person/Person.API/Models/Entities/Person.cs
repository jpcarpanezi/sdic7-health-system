using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Person.API.Models.Entities {
	[Table("Person")]
	public partial class Person {
		public Person() {
			Addresses = new HashSet<Address>();
			EmergencyContacts = new HashSet<EmergencyContact>();
		}

		[Key]
		[Column("pUUID")]
		public Guid UUID { get; set; }

		[Required]
		[Column("pName")]
		[StringLength(255)]
		public string Name { get; set; }

		[Required]
		[Column("pCPF")]
		[StringLength(14)]
		public string CPF { get; set; }

		[Required]
		[Column("pPhone")]
		[StringLength(16)]
		public string Phone { get; set; }

		[Column("pBirthDate")]
		public DateOnly BirthDate { get; set; }

		[Required]
		[Column("pEmail")]
		[StringLength(120)]
		public string Email { get; set; }

		[Required]
		[Column("pBirthCity")]
		[StringLength(100)]
		public string BirthCity { get; set; }

		[InverseProperty("PersonUUID")]
		public virtual MedicalInformation MedicalInformation { get; set; }
		[InverseProperty("PersonUUID")]
		public virtual ICollection<Address> Addresses { get; set; }
		[InverseProperty("PersonUUID")]
		public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; }
	}
}
