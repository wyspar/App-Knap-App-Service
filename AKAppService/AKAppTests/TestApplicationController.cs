using System;
using Xunit;
using AKAppService.Controllers;
using AKAppModels;
using AKAppBL;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Xunit.Abstractions;

namespace AKAppTests
{
    public class TestApplicationController
    {
        private Mock<IAppBL> appBLMock;
        private readonly ITestOutputHelper output;
        public TestApplicationController(ITestOutputHelper output)
        {
            appBLMock = new Mock<IAppBL>();
            this.output = output;
        }

        //Test to see if the controller can create an application
        [Fact]
        public void CreateAppTest()
        {
            //Arrange
            var mockRepo = new Mock<IAppBL>();
            var controller = new ApplicationController((AKAppBL.IAppBL)mockRepo.Object);

            Application application = new Application();
            application.FirstName = "dog";
            application.LastName = "dog";
            application.Email = "dog@dog";
            application.Location = new Location();
            application.Location.Address = new Address();

            //Act
            var newApp  = controller.AddAnAppAsync(application);

            //Assert
            var actionResult = Assert.IsType<Task<IActionResult>>(newApp);
            output.WriteLine("My output!!! "+actionResult.Result.ToString());
     
            Assert.Equal("Microsoft.AspNetCore.Mvc.CreatedAtActionResult", actionResult.Result.ToString());
            //var model = Assert.IsAssignableFrom<IEnumerable<Application>>(viewResult.Result);
            //Assert.Equal("dog",viewResult.);
        }
    }
}
