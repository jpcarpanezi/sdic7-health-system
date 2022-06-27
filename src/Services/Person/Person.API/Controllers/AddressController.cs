using Person.API.Models.Entities;
using Person.API.Models.Inserts;

namespace Person.API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class AddressController : ControllerBase {
		private readonly PersonContext _context;

		public AddressController(PersonContext context) {
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> InsertAddress([FromBody] InsertAddress addressInfo) {
			var person = await _context.People.Where(x => x.UUID == addressInfo.PersonUUID).FirstOrDefaultAsync();
			if (person is null) {
				return BadRequest(new MessageView("Pessoa não encontrada."));
			}

			Address address = new() {
				UUID = Guid.NewGuid(),
				PersonUUID = addressInfo.PersonUUID,
				Street = addressInfo.Street,
				City = addressInfo.City,
				Complement = addressInfo.Complement,
				State = addressInfo.State,
				ZipCode = addressInfo.ZipCode
			};

			await _context.AddAsync(address);
			await _context.SaveChangesAsync();

			return Created(nameof(GetAddress), new AddressView(address));
		}

		[HttpGet]
		[Route("{uuid:guid}")]
		public async Task<ActionResult<PaginatedItemsViewModel<AddressView>>> GetAddress([FromQuery] PaginatedItemsQuery query, Guid uuid) {
			var address = _context.Addresses.Where(x => x.PersonUUID == uuid);

			long totalItems = await address.LongCountAsync();

			var itemsOnPage = await address.Skip(query.PageSize * query.PageIndex)
								.Take(query.PageSize)
								.OrderBy(x => x.Street)
								.Select(x => new AddressView(x))
								.ToListAsync();

			return new PaginatedItemsViewModel<AddressView>(query.PageIndex, query.PageSize, totalItems, itemsOnPage);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddress addressInfo) {
			var address = await _context.Addresses.Where(s => s.UUID == addressInfo.UUID).FirstOrDefaultAsync();

			if (address is null) return BadRequest(new MessageView("Endereço não encontrado."));

			address.Street = addressInfo.Street;
			address.State = addressInfo.State;
			address.ZipCode = addressInfo.ZipCode;
			address.City = addressInfo.City;
			address.Complement = addressInfo.Complement;

			await _context.SaveChangesAsync();
			return Ok(new AddressView(address));
		}

		[HttpDelete]
		[Route("{uuid:guid}")]
		public async Task<IActionResult> DeleteAddress(Guid uuid) {
			var address = _context.Addresses.Where(s => s.UUID == uuid).FirstOrDefault();

			if (address is null) return BadRequest(new MessageView("Endereço não encontrado"));
			_context.Remove(address);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
