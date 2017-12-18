namespace HealthAndCareHospital.Test.Web.Areas.Admin.Controllers
{
    using FluentAssertions;
    using HealthAndCareHospital.Common;
    using HealthAndCareHospital.Web.Areas.Admin.Controllers;
    using Moq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;
    using HealthAndCareHospital.Services;
    using System.Threading.Tasks;
    using HealthAndCareHospital.Services.Models.Doctor;

    public class DoctorControllerTest
    {
        [Fact]
        public void DoctorControllerShoulBeInAdminArea()
        {
            //Arrange
            var controller = typeof(DoctorController);

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
        public void DoctorControllerShoulBeOnlyForAdminUser()
        {
            //Arrange
            var controller = typeof(DoctorController);

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
        public async Task PostCreateShouldReturnRedirectWithValidModel()
        {
            //Arrange
            const string nameValue = "Name";
            const string emailValue = "email@em.bg";
            const string imageURLValue = "SomeURL";
            const string specialityValue = "doctorche";
            const string departmentNameValue = "department";

            string modelName = null;
            string modelEmail = null;
            string modelImageURL = null;
            string modelSpeciality = null;
            string modelDepartmentName = null;

            var doctorService = new Mock<IDoctorService>();
            doctorService.Setup
                (d => d.CreateAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                    .Callback((string name,
                    string email, string imageURL, string speciality, string departmentName) =>
                    {
                        modelName = name;
                        modelEmail = email;
                        modelImageURL = imageURL;
                        modelSpeciality = speciality;
                        modelDepartmentName = departmentName;
                    })
                    .Returns(Task.CompletedTask);

            var controller = new DoctorController(doctorService.Object);

            // Act
            var result = await controller.Create(new DoctorViewModel
            {
                Name = nameValue,
                Email = emailValue,
                ImageURL = imageURLValue,
                Speciality = specialityValue,
                DepartmentName = departmentNameValue
            });

            // Assert
            modelName.Should().Be(nameValue);
            modelEmail.Should().Be(emailValue);
            modelImageURL.Should().Be(imageURLValue);
            modelSpeciality.Should().Be(specialityValue);
            modelDepartmentName.Should().Be(departmentNameValue);

            result.Should().BeOfType<RedirectToActionResult>();
        }
    }
}
