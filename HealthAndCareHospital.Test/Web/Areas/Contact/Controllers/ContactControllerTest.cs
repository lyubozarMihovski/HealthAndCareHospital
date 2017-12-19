namespace HealthAndCareHospital.Test.Web.Areas.Contact.Controllers
{
    using FluentAssertions;
    using HealthAndCareHospital.Common;
    using HealthAndCareHospital.Common.Areas.Contact.Controllers;
    using HealthAndCareHospital.Services;
    using HealthAndCareHospital.Services.Models.Contact;
    using HealthAndCareHospital.Web.Areas.Admin.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ContactControllerTest
    {
        [Fact]
        public void ContactControllerShoulBeOnlyForAdminAndDoctorUser()
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
            areaAttribute.Roles.Should().Be(WebConstants.AdministratorRole, WebConstants.DoctorRole);
        }

        [Fact]
        public void GetCreateShouldReturnView()
        {
            //Arrange
            var controller = new ContactController(null);

            // Act
            var result = controller.Create();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async Task PostCreateShouldReturnViewValidModel()
        {
            //Arrange
            const string nameValue = "Name";
            const string emailValue = "email@em.bg";
            const string subjectValue = "Olelele";
            const string messageValue = "Some Message";

            string modelName = null;
            string modelEmail = null;
            string modelSubject = null;
            string modelMessage = null;
            string successMessage = null;

            var contactService = new Mock<IContactService>();
            contactService.Setup
                (d => d.CreateAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                    .Callback((string name,
                    string email, string subject, string message) =>
                    {
                        modelName = name;
                        modelEmail = email;
                        modelSubject = subject;
                        modelMessage = message;
                    })
                    .Returns(Task.CompletedTask);

            var tempData = new Mock<ITempDataDictionary>();
            tempData
                .SetupSet(t => t[WebConstants.TempDataSuccessMessageKey] = It.IsAny<string>())
                .Callback((string key, object message) => successMessage = message as string);

            var controller = new ContactController(contactService.Object);
            controller.TempData = tempData.Object;
          
            // Act
            var result = await controller.Create(new ContactFormServiceModel
            {
                Name = nameValue,
                Email = emailValue,
                Subject = subjectValue,
                Message = messageValue
            });

            // Assert
            modelName.Should().Be(nameValue);
            modelEmail.Should().Be(emailValue);
            modelSubject.Should().Be(subjectValue);
            modelMessage.Should().Be(messageValue);

            result.Should().BeOfType<ViewResult>();
        }
    }
}
