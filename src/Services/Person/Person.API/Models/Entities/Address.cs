using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Person.API.Models.Entities {
	[Table("Address")]
	[Index(nameof(PersonUUID), Name = "fk_Address_Person")]
	public partial class Address {
		[Key]
		[Column("aUUID")]
		public Guid UUID { get; set; }

		[Required]
		[Column("pUUID")]
		public Guid PersonUUID { get; set; }

		[Required]
		[Column("aStreet")]
		[StringLength(255)]
		public string Street { get; set; }

		[Column("aComplement")]
		[StringLength(255)]
		public string Complement { get; set; }

		[Required]
		[Column("aCity")]
		[StringLength(100)]
		public string City { get; set; }

		[Required]
		[Column("aState")]
		[StringLength(64)]
		public string State { get; set; }

		[Required]
		[Column("aZipCode")]
		[StringLength(20)]
		public string ZipCode { get; set; }


		[ForeignKey(nameof(PersonUUID))]
		[InverseProperty(nameof(Entities.Person.Addresses))]
		public virtual Person Person { get; set; }
	}
}
