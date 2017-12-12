namespace HealthAndCareHospital.Web.Controllers
{
    using HealthAndCareHospital.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService departmentService;
        private readonly IDoctorService doctorService;

        public DepartmentsController(IDepartmentService departmentService, IDoctorService doctorService)
        {
            this.departmentService = departmentService;
            this.doctorService = doctorService;
        }

        public IActionResult All()
        {
            var departments = this.departmentService.All();
            return View(departments);
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

        public async Task<IActionResult> DoctorDetails(int id)
        {
            var doctor = await this.doctorService.DoctorExists(id);

            if (!doctor)
            {
                return NotFound();
            }
            var doc = await this.doctorService.Details(id);

            return View(doc);
        }
    }
}
