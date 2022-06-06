using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Person.API.Models.Entities {
	[Table("Address")]
	[Index("PersonUUID", Name = "fk_Address_Person")]
	public partial class Address {
		[Key]
		[Column("aUUID")]
		public Guid UUID { get; set; }

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

		[Column("aZipCode")]
		[StringLength(20)]
		public string ZipCode { get; set; }

		[ForeignKey("PersonUUID")]
		[InverseProperty("Addresses")]
		public virtual Person Person { get; set; }
	}
}
