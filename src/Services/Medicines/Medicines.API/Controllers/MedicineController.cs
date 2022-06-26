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
	public class MedicineController : ControllerBase {
		private readonly MedicinesContext _context;

		public MedicineController(MedicinesContext context) {
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> InsertMedicine([FromBody] InsertMedicine medicineInfo) {
			Models.Entities.Medicine medicine = new() {
				UUID = Guid.NewGuid(),
				DrugName = medicineInfo.DrugName,
				ActiveIngredient = medicineInfo.ActiveIngredient,
				FormRoute = medicineInfo.FormRoute,
				Company = medicineInfo.Company
			};

			await _context.AddAsync(medicine);
			await _context.SaveChangesAsync();

			return Created(nameof(GetMedicine), medicine);
		}

		[HttpGet]
		[Route("{uuid:guid}")]
		public async Task<ActionResult<PaginatedItemsViewModel<MedicineView>>> GetMedicine([FromQuery] PaginatedItemsQuery query, Guid uuid) {
			var medicine = (IQueryable<Medicine>)_context.Medicines;

			long totalItems = await medicine.LongCountAsync();

			var itemsOnPage = await medicine.Where(x => x.UUID == uuid)
								.Skip(query.PageSize * query.PageIndex)
								.Take(query.PageSize)
								.OrderBy(x => x.DrugName)
								.Select(x => new MedicineView(x))
								.ToListAsync();

			return new PaginatedItemsViewModel<MedicineView>(query.PageIndex, query.PageSize, totalItems, itemsOnPage);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateMedicine([FromBody] UpdateMedicine medicineInfo) {
			var medicine = await _context.Medicines.Where(s => s.UUID == medicineInfo.UUID).FirstOrDefaultAsync();

			if (medicine is null) return BadRequest(new MessageView("Medicamento não encontrado."));

			medicine.DrugName = medicineInfo.DrugName;
			medicine.ActiveIngredient = medicineInfo.ActiveIngredient;
			medicine.FormRoute = medicineInfo.FormRoute;
			medicine.Company = medicineInfo.Company;

			await _context.SaveChangesAsync();
			return Ok(new MedicineView(medicine));
		}

		[HttpDelete]
		[Route("{uuid:guid}")]
		public async Task<IActionResult> DeleteMedicine(Guid uuid) {
			var medicine = _context.Medicines.Where(s => s.UUID == uuid).FirstOrDefault();

			if (medicine is null) return BadRequest(new MessageView("Medicamento não encontrado"));
			_context.Remove(medicine);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
