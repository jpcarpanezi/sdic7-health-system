using System.ComponentModel.DataAnnotations;

namespace Medicines.API.Models.Inserts {
	public record InsertMedicine {
		[Required]
		public Guid UUID { get; set; }

		[Required]
		[MaxLength(255)]
		public string DrugName { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(255)]
		public string ActiveIngredient { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(255)]
		public string FormRoute { get; set; }

		[Required(AllowEmptyStrings = false)]
		[MaxLength(255)]
		public string Company { get; set; }
	}
}
