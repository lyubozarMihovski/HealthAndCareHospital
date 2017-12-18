namespace HealthAndCareHospital.Test.Web.Areas.Admin.Controllers
{
    using FluentAssertions;
    using HealthAndCareHospital.Common;
    using HealthAndCareHospital.Services;
    using HealthAndCareHospital.Services.Models.Admin;
    using HealthAndCareHospital.Web.Areas.Admin.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class MedicineControllerTest
    {
        [Fact]
        public void MedicineControllerShoulBeInAdminArea()
        {
            //Arrange
            var controller = typeof(MedicineController);

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
        public void MedicineControllerShoulBeOnlyForAdminUser()
        {
            //Arrange
            var controller = typeof(MedicineController);

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
            var controller = new MedicineController(null);

            // Act
            var result = controller.Create();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async Task PostCreateShouldReturnRedirectWithValidModel()
        {
            //Arrange
            const string nameValue = "Name";
            const string dosageValue = "3 x 2";
            const string desxriptionValue = "Some description";

            string modelName = null;
            string modelDosage = null;
            string modelDescription = null;

            var medicineService = new Mock<IMedicineService>();
            medicineService.Setup
                (d => d.Create(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                    .Callback((string name,
                    string dosage, string description) =>
                    {
                        modelName = name;
                        modelDosage = dosage;
                        modelDescription = description;
                    })
                    .Returns(Task.CompletedTask);

            var controller = new MedicineController(medicineService.Object);

            // Act
            var result = await controller.Create(new MedicineServiceModel
            {
                Name = nameValue,
                Dosage = dosageValue,
                Descritption = desxriptionValue
            });

            // Assert
            modelName.Should().Be(nameValue);
            modelDosage.Should().Be(dosageValue);
            modelDescription.Should().Be(desxriptionValue);

            result.Should().BeOfType<RedirectToActionResult>();
        }
    }
}
