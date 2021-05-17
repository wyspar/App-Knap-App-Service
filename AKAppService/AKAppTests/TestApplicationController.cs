using System;
using Xunit;
using AKAppService.Controllers;
using AKAppModels;
using AKAppBL;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AKAppTests
{
    public class TestApplicationController
    {
        private Mock<IAppBL> appBLMock;

        public TestApplicationController()
        {
            appBLMock = new Mock<IAppBL>();
        }

        [Fact]
        public void CreateAppTest()
        {
            var mockRepo = new Mock<IAppBL>();
            var controller = new ApplicationController();

            Application application = new Application();
            application.FirstName = "dog";
            application.LastName = "dog";
            application.Email = "dog@dog";
            application.Location = new Location();
            application.Location.Address = new Address();

            var result = controller.AddAnAppAsync(application);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Application>>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Count());
        }
    }
}
