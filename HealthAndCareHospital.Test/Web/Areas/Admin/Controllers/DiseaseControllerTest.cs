namespace HealthAndCareHospital.Test.Web.Areas.Admin.Controllers
{
    using FluentAssertions;
    using HealthAndCareHospital.Common;
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services;
    using HealthAndCareHospital.Services.Models.Admin;
    using HealthAndCareHospital.Web.Areas.Admin.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class DiseaseControllerTest
    {
        [Fact]
        public void DiseaseControllerShoulBeInAdminArea()
        {
            //Arrange
            var controller = typeof(DiseaseController);

            //Act
            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AreaAttribute))
                as AreaAttribute;

            //Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.RouteValue.Should().Be(WebConstants.AdminArea);
        }

        [Fact]
        public void DiseaseControllerShoulBeOnlyForAdminUser()
        {
            //Arrange
            var controller = typeof(DiseaseController);

            //Act
            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            //Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.Roles.Should().Be(WebConstants.AdministratorRole);
        }

        [Fact]
        public void GetCreateShouldReturnView()
        {
            //Arrange
            var controller = new DiseaseController(null, null);

            // Act
            var result = controller.Create();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }
    }
}
