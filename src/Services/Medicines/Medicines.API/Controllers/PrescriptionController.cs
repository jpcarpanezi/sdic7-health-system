using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Medicines.API.Models.Inserts;
using Medicines.API.Models.Entities;
using Medicines.API.Models.Views;
using Medicines.API.Models.Updates;
using Medicines.API.Models.Queries;

namespace Medicines.API.Controllers {

	[Route("api/[controller]")]
	[ApiController]
	public class PrescriptionController : ControllerBase {
		private readonly MedicinesContext _context;

		public PrescriptionController(MedicinesContext context) {
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> InsertPrescription([FromBody] InsertPrescription prescriptionInfo) {
			var medicine = await _context.Medicines.Where(x => x.UUID == prescriptionInfo.MedicineUUID).FirstOrDefaultAsync();
			if (medicine is null) {
				return BadRequest(new MessageView("Medicamento não encontrado."));
			}

			var consultation = await _context.MedicalConsultations.Where(x => x.UUID == prescriptionInfo.MedicalConsultationUUID).FirstOrDefaultAsync();
			if (consultation is null) {
				return BadRequest(new MessageView("Consulta não encontrada."));
			}

			Prescription prescription = new() {
				UUID = Guid.NewGuid(),
				MedicalConsultationUUID = prescriptionInfo.MedicalConsultationUUID,
				MedicineUUID = prescriptionInfo.MedicineUUID,
				Dosage = prescriptionInfo.Dosage
			};

			await _context.AddAsync(prescription);
			await _context.SaveChangesAsync();

			return Created(nameof(GetPrescription), prescription);
		}

		[HttpGet]
		[Route("{uuid:guid}")]
		public async Task<ActionResult<PaginatedItemsViewModel<PrescriptionView>>> GetPrescription([FromQuery] PaginatedItemsQuery query, Guid uuid) {
			var prescription = _context.Prescriptions.Where(x => x.MedicalConsultationUUID == uuid);

			long totalItems = await prescription.LongCountAsync();

			var itemsOnPage = await prescription.Skip(query.PageSize * query.PageIndex)
								.Take(query.PageSize)
								.Select(x => new PrescriptionView(x))
								.ToListAsync();

			return new PaginatedItemsViewModel<PrescriptionView>(query.PageIndex, query.PageSize, totalItems, itemsOnPage);
		}

		[HttpPut]
		public async Task<IActionResult> UpdatePrescription([FromBody] UpdatePrescription prescriptionInfo) {
			var prescription = await _context.Prescriptions.Where(s => s.UUID == prescriptionInfo.UUID).FirstOrDefaultAsync();

			if (prescription is null) return BadRequest(new MessageView("Prescrição não encontrada."));

			prescription.Dosage = prescriptionInfo.Dosage;

			await _context.SaveChangesAsync();
			return Ok(new PrescriptionView(prescription));
		}

		[HttpDelete]
		[Route("{uuid:guid}")]
		public async Task<IActionResult> DeletePrescription(Guid uuid) {
			var prescription = _context.Prescriptions.Where(s => s.UUID == uuid).FirstOrDefault();

			if (prescription is null) return BadRequest(new MessageView("Prescrição não encontrada."));
			_context.Remove(prescription);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
