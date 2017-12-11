namespace HealthAndCareHospital.Web.Areas.Doctor.Controllers
{
    using HealthAndCareHospital.Common;
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services;
    using HealthAndCareHospital.Services.Models.Doctor;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Area("Doctor")]
    [Authorize(Roles = WebConstants.AdministratorRole)]
    [Authorize(Roles = WebConstants.DoctorRole)]
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!this.User.IsInRole("Doctor"))
            {
                return Unauthorized();
            }

            var user = await this.userManager.GetUserAsync(User);
            var email = user.Email;

            await this.patientService.Create(model.Name, model.EGN, model.Age, email);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Edit(int id)
        {
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
        public async Task<IActionResult> Edit(PatientServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var patientExists = await this.patientService.PatientExists(model.Id);

            if (!patientExists)
            {
                return NotFound();
            }

            if (!this.User.IsInRole("Doctor") || !this.User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            var user = await this.userManager.GetUserAsync(User);
            var email = user.Email;

            await this.patientService.Edit(model.Id, model.Name, model.EGN, model.Age, email);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var patientExists = await this.patientService.PatientExists(id);

            if (!patientExists)
            {
                return NotFound();
            }

            var patientDelete = this.patientService.Details(id);

            return View(patientDelete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
