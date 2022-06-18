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

		[HttpPost]
		public async Task<IActionResult> InsertMedicalInformation([FromBody] InsertMedicalInformation medicalInfo) {
			Person.API.Models.Entities.MedicalInformation medicalInformation = new() {
				PersonUUID = medicalInfo.PersonUUID,
				BloodType = medicalInfo.BloodType,
				MedicalConditions = medicalInfo.MedicalConditions,
				Allergies = medicalInfo.Allergies,
				Observations = medicalInfo.Observations
			};

			await _context.AddAsync(medicalInfo);
			await _context.SaveChangesAsync();

			return Created(nameof(GetMedicalInformation), medicalInfo);
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
