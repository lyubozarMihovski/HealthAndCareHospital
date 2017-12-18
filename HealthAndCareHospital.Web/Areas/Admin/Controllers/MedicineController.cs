namespace HealthAndCareHospital.Web.Areas.Admin.Controllers
{
    using HealthAndCareHospital.Common;
    using HealthAndCareHospital.Services;
    using HealthAndCareHospital.Services.Models.Admin;
    using HealthAndCareHospital.Web.Areas.Admin.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class MedicineController : BaseAdminController
    {
        int pageSize = WebConstants.PageSize;
        private readonly IMedicineService medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            this.medicineService = medicineService;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            return View(new MedicinePageListingModel
            {
                Medicines = await this.medicineService.All(page, pageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((await this.medicineService.Total() / (double)pageSize))
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            var medicineExists = await this.medicineService
                .MedicineExists(id);

            if (!medicineExists)
            {
                return NotFound();
            }

            var medicine = await this.medicineService
                .Details(id);

            return View(medicine);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicineServiceModel model)
        {
            if (ModelState.IsValid)
            {
                await this.medicineService
                    .Create(model.Name, model.Dosage, model.Descritption);
   
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var medicineExists = await this.medicineService
                .MedicineExists(id);

            if (!medicineExists)
            {
                return NotFound();
            }

            var medicineEdit = await this.medicineService
                .Details(id);

            return View(medicineEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicineServiceModel model)
        {
            var medicineExists = await this.medicineService
                .MedicineExists(model.Id);

            if (!medicineExists)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await this.medicineService
                .Edit(model.Id, model.Name, model.Dosage, model.Descritption);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var medicineExists = await this.medicineService
                .MedicineExists(id);

            if (!medicineExists)
            {
                return NotFound();
            }

            var medicineDelete = await this.medicineService
                .Details(id);

            return View(medicineDelete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicineExists = await this.medicineService
                .MedicineExists(id);

            if (!medicineExists)
            {
                return NotFound();
            }

            var success = await this.medicineService
                .Delete(id);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
