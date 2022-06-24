namespace Medicines.API.Models.Queries {
	public class MedicalConsultationPaginatedItemsQuery {
		[Range(5, 100)]
		public int PageSize { get; set; } = 10;

		public int PageIndex { get; set; } = 0;

		public Guid PersonUUID { get; set; }
	}
}
