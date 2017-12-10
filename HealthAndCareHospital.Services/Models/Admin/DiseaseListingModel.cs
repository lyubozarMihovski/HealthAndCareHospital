namespace HealthAndCareHospital.Services.Models.Admin
{
    using System.Collections.Generic;

    public class DiseaseListingModel
    {
        public IEnumerable<DiseaseServiceModel> DiseaseListing { get; set; }

        public string SearchText { get; set; }
    }
}
