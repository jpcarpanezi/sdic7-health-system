namespace Medicines.API.Models.Updates {
	public record UpdateMedicalConsultation {
		[Required]
		public Guid UUID { get; set; }

		[Required]
		public Guid PersonUUID { get; set; }

		[Required]
		public DateTime DateTime { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(5000)]
		public string Reason { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(5000)]
		public string Diagnose { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(5000)]
		public string Observations { get; set; }
	}
}
