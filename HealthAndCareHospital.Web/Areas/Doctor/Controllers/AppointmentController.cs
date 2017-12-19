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
    using System;
    using System.Threading.Tasks;

    [Area("Doctor")]
    [Authorize(Roles = "Doctor, Administrator")]
    public class AppointmentController : Controller
    {
        private readonly IReceiptService appointmentService;
        private UserManager<User> userManager;

        public AppointmentController(IReceiptService appointmentService, UserManager<User> userManager)
        {
            this.appointmentService = appointmentService;
            this.userManager = userManager;
        }

        [Log]
        public async Task<IActionResult> All()
        {
            var user = await this.userManager.GetUserAsync(User);
            var email = user.Email;

            if (this.User.IsInRole(WebConstants.AdministratorRole))
            {
                var allAppointments = await this.appointmentService.All();
                return View(allAppointments);
            }

            var appointments = await this.appointmentService.All(email);

            return View(appointments);
        }

        [Log]
        public async Task<IActionResult> Archive()
        {
            var user = await this.userManager.GetUserAsync(User);
            var email = user.Email;

            if (this.User.IsInRole(WebConstants.AdministratorRole))
            {
                var allAppointments = await this.appointmentService.All();
                return View(allAppointments);
            }

            var appointments = await this.appointmentService.All(email);

            return View(appointments);
        }

        [Log]
        public async Task<IActionResult> New()
        {
            var user = await this.userManager.GetUserAsync(User);
            var email = user.Email;

            if (this.User.IsInRole(WebConstants.AdministratorRole))
            {
                var allAppointments = await this.appointmentService.All();
                return View(allAppointments);
            }

            var appointments = await this.appointmentService.All(email);

            return View(appointments);
        }

        [Log]
        public async Task<IActionResult> Delete(int id)
        {
            var receiptExists = await this.appointmentService.AppointmentExists(id);

            if (!receiptExists)
            {
                return NotFound();
            }

            var receipt = await this.appointmentService.Details(id);

            return View(receipt);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Log]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receiptExists = await this.appointmentService.AppointmentExists(id);

            if (!receiptExists)
            {
                return NotFound();
            }

            var success = await this.appointmentService.Delete(id);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = WebConstants.DoctorRole)]
        [Log]
        public IActionResult Create(string name)
        {
            var model = new ReceiptServiceModel
            {
                PatientName = name,
                DateTime = DateTime.UtcNow
            };
        
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WebConstants.DoctorRole)]
        [Log]
        public async Task<IActionResult> Create(ReceiptServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var user = await this.userManager.GetUserAsync(User);
            var email = user.Email;

            var success = await this.appointmentService.Create(model.PatientName, model.DateTime, email);
            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
