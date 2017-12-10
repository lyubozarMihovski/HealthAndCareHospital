namespace HealthAndCareHospital.Web.Areas.Admin.Controllers
{
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

    [Area("Admin")]
    public class DiseaseController : Controller
    {
        private readonly IDepartmentService departmentService;
        private readonly IDiseaseService diseaseService;

        public DiseaseController(IDiseaseService diseaseService, IDepartmentService departmentService)
        {
            this.diseaseService = diseaseService;
            this.departmentService = departmentService;
        }

        public async Task<IActionResult> All()
        {
            var diseases = await this.diseaseService.All();

            var diseasesModel = new DiseaseListingModel
            {
                DiseaseListing = diseases
            };

            return View(diseasesModel);
        }

        public async Task<IActionResult> Search(DiseaseListingModel model)
        {

            var diseases = await this.diseaseService.Search(model);

            return View(diseases);
        }

        public async Task<IActionResult> Details(int id)
        {
            var diseaseExists = await this.diseaseService.DiseaseExists(id);

            if (!diseaseExists)
            {
                return NotFound();
            }

            var diseaseView = await this.diseaseService.Details(id);

            if (diseaseView == null)
            {
                return NotFound();
            }

            return View(diseaseView);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiseaseServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var department = await this.departmentService.FindByName(model.DepartmentName);

            if (department == null)
            {
                return NotFound();
            }

            await this.diseaseService.Create(model.Name, model.Description, department);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var diseaseExists = await this.diseaseService.DiseaseExists(id);

            if (!diseaseExists)
            {
                return NotFound();
            }

            var disease = await this.diseaseService.Details(id);

            if (disease == null)
            {
                return NotFound();
            }

            return View(disease);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DiseaseServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (id != model.Id)
            {
                return NotFound();
            }

            var disease = await this.diseaseService.DiseaseExists(id);

            if (!disease)
            {
                return NotFound();
            }

            await this.diseaseService.Edit(id, model.Name, model.Description, model.DepartmentName);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var diseaseExists = await this.diseaseService.DiseaseExists(id);

            if (!diseaseExists)
            {
                return NotFound();
            }

            var disease = this.diseaseService.Details(id);

            return View(disease);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diseaseExists = await this.diseaseService.DiseaseExists(id);

            if (!diseaseExists)
            {
                return NotFound();
            }

            await this.diseaseService.Delete(id);
           
            return RedirectToAction(nameof(All));
        }
    }
}
