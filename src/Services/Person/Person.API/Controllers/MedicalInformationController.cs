using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Person.API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class MedicalInformationController : ControllerBase {
		private readonly PersonContext _context;

		public MedicalInformationController(PersonContext context) {
			_context = context;
		}

		private readonly string[] validBloodTypes = new string[] {
			"O-",
			"O+",
			"A-",
			"A+",
			"B-",
			"B+",
			"AB-",
			"AB+"
		};

		[HttpPost]
		public async Task<IActionResult> InsertMedicalInformation([FromBody] InsertMedicalInformation medicalInfo) {
			var person = await _context.People.Include(x => x.MedicalInformation).Where(x => x.UUID == medicalInfo.PersonUUID).FirstOrDefaultAsync();
			if (person is null) {
				return BadRequest(new MessageView("Pessoa não encontrada."));
			}

			if (person.MedicalInformation is not null) {
				return BadRequest(new MessageView("Pessoa já possui informações médicas cadastradas."));
			}

			if (!validBloodTypes.Contains(medicalInfo.BloodType)) {
				return BadRequest(new MessageView("Tipo sanguíneo inválido."));
			}

			Models.Entities.MedicalInformation medicalInformation = new() {
				PersonUUID = medicalInfo.PersonUUID,
				BloodType = medicalInfo.BloodType,
				MedicalConditions = medicalInfo.MedicalConditions,
				Allergies = medicalInfo.Allergies,
				Observations = medicalInfo.Observations
			};

			await _context.AddAsync(medicalInformation);
			await _context.SaveChangesAsync();

			return Created(nameof(GetMedicalInformation), new MedicalInformationView(medicalInformation));
		}

		[HttpGet]
		[Route("{uuid:guid}")]
		public async Task<IActionResult> GetMedicalInformation(Guid uuid) {
			var medicalInfo = await _context.MedicalInformations
								.Where(x => x.PersonUUID == uuid)
								.Select(x => new MedicalInformationView(x))
								.FirstOrDefaultAsync();

			if (medicalInfo == null) {
				return BadRequest(new MessageView("Informações médicas não encontradas"));
			}

			return Ok(medicalInfo);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateMedicalInformation([FromBody] UpdateMedicalInformation medicalInformation) {
			var medicalInfo = await _context.MedicalInformations.Where(s => s.PersonUUID == medicalInformation.PersonUUID).FirstOrDefaultAsync();

			if (medicalInfo == null) {
				return BadRequest(new MessageView("Informações médicas não encontradas."));
			}

			if (!validBloodTypes.Contains(medicalInfo.BloodType)) {
				return BadRequest(new MessageView("Tipo sanguíneo inválido."));
			}

			medicalInfo.Observations = medicalInformation.Observations;
			medicalInfo.Allergies = medicalInformation.Allergies;
			medicalInfo.MedicalConditions = medicalInformation.MedicalConditions;
			medicalInfo.BloodType = medicalInfo.BloodType;

			await _context.AddAsync(medicalInfo);
			return Ok(new MedicalInformationView(medicalInfo));
		}

		[HttpDelete]
		[Route("{uuid:guid}")]
		public async Task<IActionResult> DeleteMedicalInformation(Guid uuid) {
			var medicalInfo = await _context.MedicalInformations.Where(s => s.PersonUUID == uuid).FirstOrDefaultAsync();

			if (medicalInfo == null) {
				return BadRequest(new MessageView("Informações médicas não encontradas para serem alteradas"));
			}

			_context.Remove(medicalInfo);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
