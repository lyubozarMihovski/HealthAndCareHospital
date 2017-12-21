namespace HealthAndCareHospital.Test.Web.Areas.Doctor.Controllers
{
    using FluentAssertions;
    using HealthAndCareHospital.Web.Areas.Doctor.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;

    public class AppointmentControllerTest
    {
        [Fact]
        public void AppointmentControllerShoulBeOnlyForAdminAndDoctorUser()
        {
            //Arrange
            var controller = typeof(AppointmentController);

            //Act
            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            //Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.Roles.Should().Be("Doctor, Administrator");
        }

        [Fact]
        public void GetCreateShouldReturnView()
        {
            //Arrange
            var controller = new AppointmentController(null, null);
            var name = "Gosho";
            // Act
            var result = controller.Create(name);

            // Assert
            result.Should().BeOfType<ViewResult>();
        }
    }
}
