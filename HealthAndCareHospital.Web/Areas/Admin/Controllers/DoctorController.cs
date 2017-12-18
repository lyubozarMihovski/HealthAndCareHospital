namespace HealthAndCareHospital.Web.Areas.Admin.Controllers
{
    using HealthAndCareHospital.Services;
    using HealthAndCareHospital.Services.Models.Doctor;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class DoctorController : BaseAdminController
    {
        private readonly IDoctorService doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
        }

        public async Task<IActionResult> All()
        {
            var doctors = await this.doctorService.All();
            return View(doctors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.doctorService
                    .CreateAsync(model.Name,
                    model.Email,
                    model.ImageURL,
                    model.Speciality,
                    model.DepartmentName);

                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var doctor = await this.doctorService
                .DoctorExists(id);

            if (!doctor)
            {
                return NotFound();
            }
            var doc = await this.doctorService
                .Details(id);

            return View(doc);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await this.doctorService
                .DoctorExists(id);

            if (!doctor)
            {
                return NotFound();
            }
            var doc = await this.doctorService
                .Details(id);

            return View(doc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorViewModel model)
        {

            if (ModelState.IsValid)
            {
                var success = await this.doctorService
                    .Edit(model.Id,
                    model.Name,
                    model.Email,
                    model.ImageURL,
                    model.Speciality,
                    model.DepartmentName);
                if (!success)
                {
                    return BadRequest();
                }

                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await this.doctorService
                .DoctorExists(id);

            if (!doctor)
            {
                return NotFound();
            }
            var doc = await this.doctorService
                .Details(id);

            return View(doc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await this.doctorService
                .DoctorExists(id);

            if (!doctor)
            {
                return NotFound();
            }
            var success = await this.doctorService
                .Delete(id);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> SetToRole(int id)
        {
            var doctor = await this.doctorService.DoctorExists(id);

            if (!doctor)
            {
                return NotFound();
            }

            var model = new DoctorIdNameModel
            {
                Id = id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetToRole(DoctorIdNameModel model)
        {
            var doctor = await this.doctorService
                .DoctorExists(model.Id);

            if (!doctor)
            {
                return NotFound();
            }

            var success =  await this.doctorService
                .SetToRoleAsync(model.Id);

            if (success)
            {
                TempData["Message"] = $"This {model.Name} doctor was set in Doctor role";
            }
            if (!success)
            {
                TempData["Message"] = $"This {model.Name} doctor has no user!";
            }

            return RedirectToAction(nameof(All));
        }
    }
}