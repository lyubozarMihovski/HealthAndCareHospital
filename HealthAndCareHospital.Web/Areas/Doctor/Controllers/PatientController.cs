namespace HealthAndCareHospital.Web.Areas.Doctor.Controllers
{
    using HealthAndCareHospital.Common;
    using HealthAndCareHospital.Common.Infrastructure.Filters;
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services;
    using HealthAndCareHospital.Services.Models.Doctor;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Area("Doctor")]
    [Authorize(Roles = "Doctor, Administrator")]
    public class PatientController : Controller
    {
        private readonly IPatientService patientService;
        private UserManager<User> userManager;

        public PatientController(IPatientService patientService, UserManager<User> userManager)
        {
            this.patientService = patientService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All()
        {
            var patients = await this.patientService.All();

            return View(patients);
        }

        public async Task<IActionResult> Details(int id)
        {
            var patientExists = await this.patientService.PatientExists(id);

            if (!patientExists)
            {
                return NotFound();
            }

            var patient = await this.patientService.Details(id);

            return View(patient);
        }

        [Authorize(Roles = WebConstants.DoctorRole)]
        [Log]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WebConstants.DoctorRole)]
        [Log]
        public async Task<IActionResult> Create(PatientServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await this.userManager.GetUserAsync(User);
            var email = user.Email;

            var success = await this.patientService.Create(model.Name, model.EGN, model.Age, email);
            if (!success)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = WebConstants.DoctorRole)]
        [Log]
        public async Task<IActionResult> Edit(int id)
        {
            if (!this.User.IsInRole(WebConstants.DoctorRole))
            {
                return NotFound();
            }

            var patientExists = await this.patientService.PatientExists(id);

            if (!patientExists)
            {
                return NotFound();
            }

            var patientEdit = await this.patientService.Details(id);

            return View(patientEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WebConstants.DoctorRole)]
        [Log]
        public async Task<IActionResult> Edit(PatientServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!this.User.IsInRole(WebConstants.DoctorRole))
            {
                return Unauthorized();
            }

            var patientExists = await this.patientService.PatientExists(model.Id);

            if (!patientExists)
            {
                return NotFound();
            }

            var user = await this.userManager.GetUserAsync(User);
            var email = user.Email;

            var success = await this.patientService.Edit(model.Id, model.Name, model.EGN, model.Age, email);
            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        [Log]
        public async Task<IActionResult> Delete(int id)
        {
            var patientExists = await this.patientService.PatientExists(id);

            if (!patientExists)
            {
                return NotFound();
            }

            var patientDelete = await this.patientService.Details(id);

            return View(patientDelete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Log]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patientExists = await this.patientService.PatientExists(id);

            if (!patientExists)
            {
                return NotFound();
            }

            await this.patientService.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
