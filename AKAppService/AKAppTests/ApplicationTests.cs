using System;
using Xunit;
using AKAppService.Controllers;
using AKAppModels;
namespace AKAppTests
{
    public class ApplicationTests
    {
        [Theory]
        [InlineData("douglas","richardson","wyspar@gmail.com")]
        [InlineData("rob","whit","no@no")]
        [InlineData(null,null,null)]
        //Checks to see if an application is valid when it is created
        public void ApplicationShouldBeValid(string firstName, string lastName, string email)
        {
            Application application = new Application();
            
            if (email != null && firstName != null && lastName != null)
            {
                application.FirstName = firstName;
                application.LastName = lastName;
                application.Email = email;
                Assert.Equal(firstName,application.FirstName);
                Assert.Equal(lastName,application.LastName);
                Assert.Equal(email,application.Email);
            }
            else
            {
                Assert.Throws<ArgumentNullException>(() => application.Email = email);
                Assert.Throws<ArgumentNullException>(() => application.FirstName = firstName);
                Assert.Throws<ArgumentNullException>(() => application.LastName = lastName);
            }
        }

        [Theory]
        [InlineData("no")]
        [InlineData("")]
        public void ApplicationEmailNotShouldBeValid(string email) {
            Application application = new Application();
            Assert.Throws<ArgumentNullException>(() => application.Email = email);
        }

        [Theory]
        [InlineData("Columns",1000,true)]
        [InlineData(null, -12, true)]
        //Checks to see if location is valid when it is being created
        public void LocationShouldBeValid(string name, int cost, bool rent)
        {
            Location location = new Location();
            if (name != null && cost > 0)
            {    
                location.Name = name;
                location.Cost = cost;
                location.RentOrBuy = rent;
                Assert.Equal(rent, location.RentOrBuy);
                Assert.Equal(cost, location.Cost);
                Assert.Equal(name, location.Name);
            }
            else
            {
                location.RentOrBuy = rent;
                Assert.Equal(rent, location.RentOrBuy);
                Assert.Throws<ArgumentNullException>(() => location.Name = name);
                Assert.Throws<ArgumentNullException>(() => location.Cost = cost);
            }
                
        }

        [Theory]
        [InlineData("myId", 1, 1)]
        [InlineData(" ", 0, -3)]
        //An upload should be valid
        public void UploadShouldBeValid(string filename, int blobID, int applicationID)
        {
            Upload upload = new Upload();
            if (filename != null && blobID > 0 && applicationID > 0)
            {
                upload.FileName = filename;
                upload.BlobID = blobID;
                upload.ApplicationID = applicationID;
                Assert.Equal(applicationID, upload.ApplicationID);
                Assert.Equal(blobID, upload.BlobID);
                Assert.Equal(filename, upload.FileName);
            }
            else
            {
                Assert.Throws<ArgumentNullException>(() => upload.FileName = filename);
                Assert.Throws<ArgumentNullException>(() => upload.ApplicationID = applicationID);
                Assert.Throws<ArgumentNullException>(() => upload.BlobID = blobID);
            }
        }
    }
}
