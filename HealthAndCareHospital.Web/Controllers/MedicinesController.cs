namespace HealthAndCareHospital.Web.Controllers
{
    using HealthAndCareHospital.Common;
    using HealthAndCareHospital.Services;
    using HealthAndCareHospital.Web.Areas.Admin.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Authorize]
    public class MedicinesController : Controller
    {
        int pageSize = WebConstants.PageSize;
        private readonly IMedicineService medicineService;

        public MedicinesController(IMedicineService medicineService)
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
