namespace Medicines.API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class MedicalConsultationController : ControllerBase {
		private readonly MedicinesContext _context;

		public MedicalConsultationController(MedicinesContext context) {
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> InsertMedicalConsultation([FromBody] InsertMedicalConsultation insert) {
			var consultation = new MedicalConsultation() {
				UUID = Guid.NewGuid(),
				PersonUUID = insert.PersonUUID,
				DateTime = insert.DateTime,
				Reason = insert.Reason,
				Diagnose = insert.Diagnose,
				Observations = insert.Observations
			};

			_context.Add(consultation);
			await _context.SaveChangesAsync();

			return Created(nameof(GetMedicalConsultationByParam), new MedicalConsultationView(consultation));
		}

		[HttpGet]
		public async Task<ActionResult<PaginatedItemsViewModel<MedicalConsultationView>>> GetMedicalConsultation([FromQuery] MedicalConsultationPaginatedItemsQuery query) {
			var root = (IQueryable<MedicalConsultation>)_context.MedicalConsultations.Where(x => x.PersonUUID == query.PersonUUID);

			long totalItems = await root.LongCountAsync();

			var itemsOnPage = await root.Skip(query.PageSize * query.PageIndex)
							   .Take(query.PageSize)
							   .OrderByDescending(x => x.DateTime)
							   .Select(x => new MedicalConsultationView(x))
							   .ToListAsync();

			return new PaginatedItemsViewModel<MedicalConsultationView>(query.PageIndex, query.PageSize, totalItems, itemsOnPage);
		}

		[HttpGet]
		[Route("item/{uuid:guid}")]
		public async Task<IActionResult> GetMedicalConsultationByParam(Guid uuid) {
			var consultation = await _context.MedicalConsultations.Where(x => x.UUID == uuid).FirstOrDefaultAsync();

			if (consultation is null) {
				return NotFound();
			}

			return Ok(new MedicalConsultationView(consultation));
		}

		[HttpPatch]
		public async Task<IActionResult> UpdateMedicalConsultation([FromBody] UpdateMedicalConsultation update) {
			var consultation = await _context.MedicalConsultations.Where(x => x.UUID == update.UUID).FirstOrDefaultAsync();

			if (consultation is null) {
				return NotFound();
			}

			consultation.PersonUUID = update.PersonUUID;
			consultation.DateTime = update.DateTime;
			consultation.Reason = update.Reason;
			consultation.Diagnose = update.Diagnose;
			consultation.Observations = update.Observations;

			await _context.SaveChangesAsync();

			return Ok(new MedicalConsultationView(consultation));
		}

		[HttpGet]
		[Route("ping")]
		public IActionResult Ping() {
			return Ok("Pong");
		}
	}
}
