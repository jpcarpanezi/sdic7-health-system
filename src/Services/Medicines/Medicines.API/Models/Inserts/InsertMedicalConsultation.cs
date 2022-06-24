namespace Medicines.API.Models.Inserts {
	public record InsertMedicalConsultation {
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
