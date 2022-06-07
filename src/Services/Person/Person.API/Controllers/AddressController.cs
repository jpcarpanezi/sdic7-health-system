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

			return Created(nameof(GetAddress), address);
		}

		[HttpGet]
		[Route("{uuid:guid}")]
		public async Task<IActionResult> GetAddress(Guid uuid) {
			Address address = new();
			address = _context.Addresses.Where(s => s.PersonUUID == uuid).Single();
			return Ok(address);
		}
	}
}
