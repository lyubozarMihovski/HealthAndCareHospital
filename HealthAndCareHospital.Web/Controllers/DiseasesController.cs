namespace HealthAndCareHospital.Web.Controllers
{
    using HealthAndCareHospital.Services;
    using HealthAndCareHospital.Services.Models.Admin;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class DiseasesController : Controller
    {
        private readonly IDepartmentService departmentService;
        private readonly IDiseaseService diseaseService;

        public DiseasesController(IDiseaseService diseaseService, IDepartmentService departmentService)
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
            if (String.IsNullOrWhiteSpace(model.SearchText))
            {
                return RedirectToAction(nameof(All));
            }

            var diseases = await this.diseaseService.Search(model.SearchText);

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
    }
}