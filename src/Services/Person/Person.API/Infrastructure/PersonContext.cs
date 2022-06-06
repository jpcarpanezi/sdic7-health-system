using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Person.API.Models.Entities;

namespace Person.API.Infrastructure {
	public partial class PersonContext : DbContext {
		public PersonContext() {
		}

		public PersonContext(DbContextOptions<PersonContext> options)
			: base(options) {
		}

		public virtual DbSet<Address> Addresses { get; set; }
		public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; }
		public virtual DbSet<MedicalInformation> MedicalInformations { get; set; }
		public virtual DbSet<Models.Entities.Person> People { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.HasCharSet("utf8mb4")
				.UseCollation("utf8mb4_0900_ai_ci");

			modelBuilder.Entity<Address>(entity => {
				entity.HasKey(e => e.UUID)
					.HasName("PRIMARY");

				entity.HasOne(d => d.Person)
					.WithMany(p => p.Addresses)
					.HasForeignKey(d => d.PersonUUID)
					.HasConstraintName("fk_Address_Person");
			});

			modelBuilder.Entity<EmergencyContact>(entity => {
				entity.HasKey(e => e.UUID)
					.HasName("PRIMARY");

				entity.HasOne(d => d.Person)
					.WithMany(p => p.EmergencyContacts)
					.HasForeignKey(d => d.PersonUUID)
					.HasConstraintName("fk_EmergencyContacts_Person");
			});

			modelBuilder.Entity<MedicalInformation>(entity => {
				entity.HasKey(e => e.PersonUUID)
					.HasName("PRIMARY");

				entity.Property(e => e.PersonUUID).ValueGeneratedOnAdd();

				entity.HasOne(d => d.Person)
					.WithOne(p => p.MedicalInformation)
					.HasForeignKey<MedicalInformation>(d => d.PersonUUID)
					.HasConstraintName("fk_MedicalInformations_Person");
			});

			modelBuilder.Entity<Models.Entities.Person>(entity => {
				entity.HasKey(e => e.PUuid)
					.HasName("PRIMARY");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
