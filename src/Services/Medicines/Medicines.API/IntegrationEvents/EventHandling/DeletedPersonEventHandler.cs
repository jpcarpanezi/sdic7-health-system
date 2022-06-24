using Serilog.Context;

namespace Medicines.API.IntegrationEvents.EventHandling {
	public class DeletedPersonEventHandler : IIntegrationEventHandler<DeletedPersonEvent> {
		private readonly ILogger<DeletedPersonEventHandler> _logger;
		private readonly MedicinesContext _context;

		public DeletedPersonEventHandler(MedicinesContext context, ILogger<DeletedPersonEventHandler> logger) {
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task Handle(DeletedPersonEvent @event) {
			using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-Medicines")) {
				_logger.LogInformation("Handling integration event: {eventId} at Medicines", @event.Id);

				var consultations = await _context.MedicalConsultations.Where(x => x.PersonUUID == @event.PersonUUID).ToListAsync();
				int consultationCount = consultations.Count;

				if (consultationCount == 0) {
					_logger.LogInformation("No medical consultations registered for person UUID {uuid}", @event.PersonUUID);
					return;
				}

				_context.RemoveRange(consultations);
				await _context.SaveChangesAsync();
				_logger.LogInformation("Removed {consultationCount} consultations", consultationCount);
			}
		}
	}
}
