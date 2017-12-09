namespace HealthAndCareHospital.Web.Areas.Admin.Controllers
{
    using HealthAndCareHospital.Services;
    using HealthAndCareHospital.Services.Models.Doctor;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class DoctorController : Controller
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
                    .CreateAsync(model.Name, model.Email, model.ImageURL, model.Speciality, model.DepartmentName);
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

    }
}