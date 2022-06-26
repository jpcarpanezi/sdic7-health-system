using System.Text.RegularExpressions;

namespace Person.API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class PersonController : ControllerBase {
		private readonly PersonContext _context;
		public readonly IEventBus _eventBus;
		public readonly ILogger<PersonController> _logger;

		public PersonController(PersonContext context, IEventBus eventBus, ILogger<PersonController> logger) {
			_context = context;
			_eventBus = eventBus;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> InsertPerson([FromBody] InsertPerson insert) {
			if (!ValidateCPF(insert.CPF)) {
				return BadRequest(new MessageView("CPF Inválido"));
			}

			var query = await _context.People.Where(x => x.CPF == insert.CPF || x.Email == insert.Email).ToListAsync();

			if (query.Any(x => x.CPF == insert.CPF)) {
				return BadRequest(new MessageView("Este CPF já foi cadastrado"));
			}

			if (query.Any(x => x.Email == insert.Email)) {
				return BadRequest(new MessageView("Este e-mail já foi cadastrado"));
			}

			Models.Entities.Person person = new() {
				UUID = Guid.NewGuid(),
				Name = insert.Name,
				CPF = insert.CPF.ToString(),
				Phone = insert.Phone.ToString(),
				BirthDate = insert.BirthDate,
				Email = insert.Email,
				BirthCity = insert.BirthCity
			};

			_context.Add(person);
			await _context.SaveChangesAsync();

			return Created(nameof(GetPerson), new PersonView(person));
		}

		[HttpGet]
		public async Task<ActionResult<PaginatedItemsViewModel<PersonView>>> GetPeople([FromQuery] PaginatedItemsQuery query) {
			var root = (IQueryable<Models.Entities.Person>)_context.People;

			long totalItems = await root.LongCountAsync();

			var itemsOnPage = await root.Skip(query.PageSize * query.PageIndex)
							   .Take(query.PageSize)
							   .OrderBy(x => x.Name)
							   .Select(x => new PersonView(x))
							   .ToListAsync();

			return new PaginatedItemsViewModel<PersonView>(query.PageIndex, query.PageSize, totalItems, itemsOnPage);
		}

		[HttpGet]
		[Route("item")]
		public async Task<IActionResult> GetPerson([FromQuery] SearchPerson search) {
			var condition = SearchPerson.SearchCondition(search.Method, search.Value);

			var person = await _context.People.Where(condition).Select(x => new PersonView(x)).FirstOrDefaultAsync();

			if (person is null) {
				return NotFound();
			}

			return Ok(person);
		}

		[HttpPatch]
		public async Task<IActionResult> UpdatePerson([FromBody] UpdatePerson update) {
			if (await _context.People.AnyAsync(x => x.Email == update.Email && x.UUID != update.UUID)) {
				return BadRequest(new MessageView("Este e-mail já foi cadastrado."));
			}

			var person = await _context.People.Where(x => x.UUID == update.UUID).FirstOrDefaultAsync();

			if (person is null) {
				return NotFound();
			}

			person.Name = update.Name;
			person.Phone = update.Phone;
			person.BirthDate = update.BirthDate;
			person.Email = update.Email;
			person.BirthCity = update.BirthCity;

			await _context.SaveChangesAsync();

			return Ok(new PersonView(person));
		}

		[HttpDelete]
		public async Task<IActionResult> DeletePerson([FromQuery] SearchPerson search) {
			var condition = SearchPerson.SearchCondition(search.Method, search.Value);

			var person = await _context.People.Where(condition).FirstOrDefaultAsync();

			if (person is null) {
				return NotFound();
			}

			_context.Remove(person);
			await _context.SaveChangesAsync();

			var eventMsg = new DeletedPersonEvent { PersonUUID = person.UUID };

			try {
				_eventBus.Publish(eventMsg);
			} catch (Exception e) {
				_logger.LogCritical(e, "Failed to publish event {eventName} for user {personUUID}", nameof(DeletedPersonEvent), person.UUID);
			}

			return NoContent();
		}

		private static bool ValidateCPF(string document) {
			const string regexStr = "^[0-9]{11}$";

			if (document.Length != 11) {
				return false;
			}

			var regex = new Regex(regexStr, RegexOptions.None, TimeSpan.FromSeconds(30));
			if (!regex.IsMatch(document)) {
				return false;
			}

			ushort[] numArr = Array.ConvertAll(document.ToCharArray(), (ch) => ushort.Parse(ch.ToString()));

			for (int i = 9; i < 11; i++) {
				int j = 0, k = 0;
				for (; k < i; k++) {
					j += numArr[k] * (i + 1 - k);
				}

				j = 10 * j % 11 % 10;

				if (numArr[k] != j) {
					return false;
				}
			}

			return true;
		}
	}
}
