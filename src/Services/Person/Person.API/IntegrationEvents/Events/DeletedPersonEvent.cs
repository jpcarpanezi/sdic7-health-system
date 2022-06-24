namespace Person.API.IntegrationEvents.Events {
	public record DeletedPersonEvent : IntegrationEvent {
		public Guid PersonUUID { get; set; }

		public DeletedPersonEvent() { }

		public DeletedPersonEvent(Guid personUUID) {
			PersonUUID = personUUID;
		}
	}
}
