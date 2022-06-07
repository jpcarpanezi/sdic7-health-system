namespace Person.API.Models.Views {
	public record PersonView {
		public Guid UUID { get; set; }

		public string Name { get; set; }

		public string CPF { get; set; }

		public string Phone { get; set; }
		
		public DateTime BirthDate { get; set; }

		public string Email { get; set; }

		public string BirthCity { get; set; }

		public PersonView(Entities.Person person) {
			UUID = person.UUID;
			Name = person.Name;
			CPF = person.CPF;
			Phone = person.Phone;
			BirthDate = person.BirthDate;
			Email = person.Email;
			BirthCity = person.BirthCity;
		}
	}
}
