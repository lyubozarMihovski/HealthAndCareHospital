namespace HealthAndCareHospital.Web.Areas.Admin.Controllers
{
    using HealthAndCareHospital.Services;
    using HealthAndCareHospital.Services.Models.Admin;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        public async Task<IActionResult> All()
        {
            var departments = await this.departmentService.All();
            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentCreateServiceModel model)
        {
            if (ModelState.IsValid)
            {
                await this.departmentService.
                    CreateAsync(model.Name, model.Description, model.ImageURL); 
                return RedirectToAction(nameof(Create));
            }
            return View(model);
        }
    }
}