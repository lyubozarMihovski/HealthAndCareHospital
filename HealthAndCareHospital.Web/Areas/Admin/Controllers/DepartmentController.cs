namespace HealthAndCareHospital.Web.Areas.Admin.Controllers
{
    using HealthAndCareHospital.Common;
    using HealthAndCareHospital.Services;
    using HealthAndCareHospital.Services.Models.Admin;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Area("Admin")]
    [Authorize(Roles = WebConstants.AdministratorRole)]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        [AllowAnonymous]
        public IActionResult All()
        {
            var departments = this.departmentService.All();
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

                return RedirectToAction(nameof(All));
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var department = await this.departmentService.DepartmentExists(id);

            if (!department)
            {
                return NotFound();
            }

            var departmentDetails = await this.departmentService.Details(id);

            return View(departmentDetails);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var department = await this.departmentService.DepartmentExists(id);

            if (!department)
            {
                return NotFound();
            }

            var departmentDelete = await this.departmentService.Details(id);

            return View(departmentDelete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await this.departmentService.DepartmentExists(id);

            if (!department)
            {
                return NotFound();
            }

            await this.departmentService.Delete(id);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var department = await this.departmentService.DepartmentExists(id);

            if (!department)
            {
                return NotFound();
            }

            var departmentEdit = await this.departmentService.Details(id);

            return View(departmentEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentCreateServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var department = await this.departmentService.DepartmentExists(model.Id);

            if (!department)
            {
                return NotFound();
            }

            await this.departmentService.Edit(model.Id,
                model.Name, model.Description, model.ImageURL);

            return RedirectToAction(nameof(All));
        }
    }
}