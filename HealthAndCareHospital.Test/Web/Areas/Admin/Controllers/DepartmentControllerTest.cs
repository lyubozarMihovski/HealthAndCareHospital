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

    public class DepartmentControllerTest
    {
        [Fact]
        public void DepartmnetControllerShoulBeInAdminArea()
        {
            //Arrange
            var controller = typeof(DepartmentController);

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
        public void DepartmnetControllerShoulBeOnlyForAdminUser()
        {
            //Arrange
            var controller = typeof(DepartmentController);

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
            var controller = new DepartmentController(null);

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
            const string descriptionValue = "Some description";
            const string imageURLValue = "SomeURL";

            string modelName = null;
            string modelDescription = null;
            string modelImageURL = null;

            var departmentService = new Mock<IDepartmentService>();
            departmentService.Setup
                (d => d.CreateAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                    .Callback((string name,
                    string description, string imageURL) =>
                    {
                        modelName = name;
                        modelImageURL = imageURL;
                        modelDescription = description;
                    })
                    .Returns(Task.CompletedTask);

            var controller = new DepartmentController(departmentService.Object);

            // Act
            var result = await controller.Create(new DepartmentCreateServiceModel
            {
                Name = nameValue,
                Description = descriptionValue,
                ImageURL = imageURLValue
            });

            // Assert
            modelName.Should().Be(nameValue);
            modelDescription.Should().Be(descriptionValue);
            modelImageURL.Should().Be(imageURLValue);

            result.Should().BeOfType<RedirectToActionResult>();
        }
    }
}
