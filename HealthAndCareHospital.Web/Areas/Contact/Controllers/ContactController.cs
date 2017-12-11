namespace HealthAndCareHospital.Common.Areas.Contact.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using HealthAndCareHospital.Data;
    using Microsoft.AspNetCore.Authorization;
    using HealthAndCareHospital.Services;
    using HealthAndCareHospital.Services.Models.Contact;
    using System.Threading.Tasks;

    [Area("Contact")]
    [Authorize(Roles = WebConstants.AdministratorRole)]
    [Authorize(Roles = WebConstants.DoctorRole)]
    public class ContactController : Controller
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        public async Task<IActionResult> All()
        {
            var contacts = await this.contactService.All();
            return View(contacts);
        }

        [Route("/Contact/Create")]
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("/Contact/Create")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create(ContactFormServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "There was a problem with your contact data";
                return View();
            }
            try
            {
                await this.contactService
                    .CreateAsync(model.Name, model.Email, model.Subject, model.Message);
                TempData["Message"] = "Your contact form has been send";
                return View();
            }
            catch
            {
                TempData["Message"] = "There was a problem with your contact data";
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ContactFormServiceModel model)
        {
            try
            {
               await this.contactService.DeleteAsync(id);
             

                return RedirectToAction(nameof(All));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await this.contactService.CreateModelAsync(id);
            return View(model);
        }
    }
}