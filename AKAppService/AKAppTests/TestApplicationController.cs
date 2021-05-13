/*using System;
using Xunit;
using AKAppService.Controllers;
using AKAppModels;
using AKAppBL;
using Moq;
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

            var result = controller.AddAnAppAsync();

                //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<HeroIndexVM>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
*/