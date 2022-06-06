using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Medicines.API.Models.Entities;

namespace Medicines.API.Infrastructure {
	public partial class MedicinesContext : DbContext {
		public MedicinesContext() {
		}

		public MedicinesContext(DbContextOptions<MedicinesContext> options)
			: base(options) {
		}

		public virtual DbSet<MedicalConsultation> MedicalConsultations { get; set; }
		public virtual DbSet<Medicine> Medicines { get; set; }
		public virtual DbSet<Prescription> Prescriptions { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.HasCharSet("utf8mb4")
				.UseCollation("utf8mb4_0900_ai_ci");

			modelBuilder.Entity<MedicalConsultation>(entity => {
				entity.HasKey(e => e.UUID)
					.HasName("PRIMARY");
			});

			modelBuilder.Entity<Medicine>(entity => {
				entity.HasKey(e => e.UUID)
					.HasName("PRIMARY");
			});

			modelBuilder.Entity<Prescription>(entity => {
				entity.HasKey(e => e.UUID)
					.HasName("PRIMARY");

				entity.HasOne(d => d.Medicine)
					.WithMany(p => p.Prescriptions)
					.HasForeignKey(d => d.MedicineUUID)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_Prescriptions_Medicines");

				entity.HasOne(d => d.MedicalConsultation)
					.WithMany(p => p.Prescriptions)
					.HasForeignKey(d => d.MedicalConsultationUUID)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_Prescriptions_MedicalConsultation");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
