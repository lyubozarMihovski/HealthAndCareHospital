namespace HealthAndCareHospital.Web.Controllers
{
    using HealthAndCareHospital.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class MedicinesController : Controller
    {
        private readonly IMedicineService medicineService;

        public MedicinesController(IMedicineService medicineService)
        {
            this.medicineService = medicineService;
        }

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
    }
}
