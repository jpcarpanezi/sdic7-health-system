namespace Medicines.API.Models.Views {
	public record MedicineView {
		public Guid UUID { get; set; }

		public string DrugName { get; set; }

		public string ActiveIngredient { get; set; }

		public string FormRoute { get; set; }

		public string Company { get; set; }

		public MedicineView(Entities.Medicine medicine) { 
			UUID = medicine.UUID;
			DrugName = medicine.DrugName;
			ActiveIngredient = medicine.ActiveIngredient;
			FormRoute = medicine.FormRoute;
			Company = medicine.Company;
		}

	}
}
