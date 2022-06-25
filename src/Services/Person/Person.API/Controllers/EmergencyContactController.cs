using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Person.API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class EmergencyContactController : ControllerBase {
		private readonly PersonContext _context;

		public EmergencyContactController(PersonContext context) {
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> InsertEmergencyContact([FromBody] InsertEmergencyContact emergencyContactInfo) {
			Person.API.Models.Entities.EmergencyContact emergencyContact  = new() {
				UUID = Guid.NewGuid(),
				PersonUUID = emergencyContactInfo.PersonUUID,
				Name = emergencyContactInfo.Name,
				Phone = emergencyContactInfo?.Phone,
				Kinship = emergencyContactInfo?.Kinship
			};

			await _context.AddAsync(emergencyContact);
			await _context.SaveChangesAsync();

			return Created(nameof(GetEmergencyContact), emergencyContact);
		}

		[HttpGet]
		[Route("{uuid:guid}")]
		public async Task<ActionResult<PaginatedItemsViewModel<EmergencyContactView>>> GetEmergencyContact([FromQuery] PaginatedItemsQuery query, Guid uuid) {
			var emergencyContact = (IQueryable<Person.API.Models.Entities.EmergencyContact>)_context.EmergencyContacts;

			long totalItems = await emergencyContact.LongCountAsync();

			var itemsOnPage = await emergencyContact.Where(x => x.PersonUUID == uuid)
								.Skip(query.PageSize * query.PageIndex)
								.Take(query.PageSize)
								.OrderBy(x => x.Name)
								.Select(x => new EmergencyContactView(x))
								.ToListAsync();

			return new PaginatedItemsViewModel<EmergencyContactView>(query.PageIndex, query.PageSize, totalItems, itemsOnPage);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateEmergencyContact([FromBody] UpdateEmergencyContact emergencyContactInfo) {
			var emergencyContact = await _context.EmergencyContacts.Where(s => s.UUID == emergencyContactInfo.UUID).FirstOrDefaultAsync();

			if (emergencyContact is null) return BadRequest(new MessageView("Contato de emergência não encontrado."));

			emergencyContact.Phone = emergencyContactInfo.Phone;
			emergencyContact.Name = emergencyContactInfo.Name;
			emergencyContact.Kinship = emergencyContactInfo.Kinship;

			await _context.SaveChangesAsync();
			return Ok(new EmergencyContactView(emergencyContact));
		}

		[HttpDelete]
		[Route("{uuid:guid}")]
		public async Task<IActionResult> DeleteEmergencyContat(Guid uuid) {
			var emergencyContact = _context.EmergencyContacts.Where(s => s.UUID == uuid).FirstOrDefault();

			if (emergencyContact is null) return BadRequest(new MessageView("Contato de emergência não encontrado"));
			_context.Remove(emergencyContact);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
