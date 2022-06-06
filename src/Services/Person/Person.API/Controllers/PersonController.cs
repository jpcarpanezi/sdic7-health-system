namespace Person.API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class PersonController : ControllerBase {
		private readonly PersonContext _context;

		public PersonController(PersonContext context) { 
			_context = context;
		}

		public async Task<IActionResult> InsertPerson() {
			return Ok();
		}

		public async Task<IActionResult> GetPeople() {
			return Ok();
		}

		public async Task<IActionResult> GetPersonByParam() {
			return Ok();
		}

		public async Task<IActionResult> UpdatePerson() {
			return Ok();
		}

		public async Task<IActionResult> DeletePerson() {
			return Ok();
		}
	}
}
