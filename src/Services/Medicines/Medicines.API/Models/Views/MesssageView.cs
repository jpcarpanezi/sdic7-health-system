namespace Medicines.API.Models.Views {
	public record MessageView {
		public string Message { get; private set; }

		public MessageView(string message) {
			Message = message;
		}
	}
}
