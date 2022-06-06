using System;
using System.Collections.Generic;
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

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			if (!optionsBuilder.IsConfigured) {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
				optionsBuilder.UseMySql("server=localhost;port=3306;database=HealthPerson;uid=root;pwd=1234;guidformat=Binary16", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"));
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
				.HasCharSet("utf8mb4");

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
					.HasForeignKey(d => d.PUuid)
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
				entity.HasKey(e => e.UUID)
					.HasName("PRIMARY");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
