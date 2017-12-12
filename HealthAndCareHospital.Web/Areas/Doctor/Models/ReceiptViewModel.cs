namespace HealthAndCareHospital.Web.Areas.Doctor.Models
{
    using HealthAndCareHospital.Services.Models.Doctor;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class ReceiptViewModel
    {
        public ReceiptServiceModel Receipt { get; set; }

        public IEnumerable<SelectListItem> Medicines { get; set; }
    }
}
