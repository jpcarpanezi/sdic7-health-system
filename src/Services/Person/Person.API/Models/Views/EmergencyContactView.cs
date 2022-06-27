namespace Person.API.Models.Views {
	public record EmergencyContactView {
		public Guid UUID { get; set; }

		public Guid PersonUUID { get; set; }

		public string Name { get; set; }

		public string Phone { get; set; }

		public string Kinship { get; set; }

		public EmergencyContactView(Entities.EmergencyContact emergencyContact) {
			UUID = emergencyContact.UUID;
			PersonUUID = emergencyContact.PersonUUID;
			Name = emergencyContact.Name;
			Phone = emergencyContact.Phone;
			Kinship = emergencyContact.Kinship;
		}
	}
}
