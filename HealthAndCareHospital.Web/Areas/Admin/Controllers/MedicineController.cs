using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthAndCareHospital.Data;
using HealthAndCareHospital.Data.Models;
using HealthAndCareHospital.Services;
using HealthAndCareHospital.Services.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using HealthAndCareHospital.Common;

namespace HealthAndCareHospital.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebConstants.AdministratorRole)]
    public class MedicineController : Controller
    {
        private readonly IMedicineService medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            this.medicineService = medicineService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var medicines = await this.medicineService.All();
            return View(medicines);
        }

        public async Task<IActionResult> Details(int id)
        {
            var medicineExists = await this.medicineService.MedicineExists(id);

            if (!medicineExists)
            {
                return NotFound();
            }

            var medicine = await this.medicineService.Details(id);

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
                await this.medicineService.Create(model.Name, model.Dosage, model.Descritption);
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var medicineExists = await this.medicineService.MedicineExists(id);

            if (!medicineExists)
            {
                return NotFound();
            }

            var medicineEdit = await this.medicineService.Details(id);

            return View(medicineEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicineServiceModel model)
        {
            var medicineExists = await this.medicineService.MedicineExists(model.Id);

            if (!medicineExists)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.medicineService.Edit(model.Id, model.Name, model.Dosage, model.Descritption);

            return RedirectToAction(nameof(All));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var medicineExists = await this.medicineService.MedicineExists(id);

            if (!medicineExists)
            {
                return NotFound();
            }

            var medicineDelete = await this.medicineService.Details(id);

            return View(medicineDelete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicineExists = await this.medicineService.MedicineExists(id);

            if (!medicineExists)
            {
                return NotFound();
            }

            await this.medicineService.Delete(id);
            return RedirectToAction(nameof(All));
        }
    }
}
