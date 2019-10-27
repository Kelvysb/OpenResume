using System;
using Xunit;
using OpenResumeAPI.Models;
using System.Collections.Generic;
using OpenResumeAPI.Controllers;
using OpenResumeAPI.Services;
using OpenResumeAPI.Business;
using OpenResumeAPI.Helpers;
using OpenResumeAPI.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using OpenResumeAPI.Services.Interfaces;

namespace OpenResumeAPI.Tests
{
    public class UserTests
    {        
        UserController userController;
        List<User> expectedUsers;
        List<User> returnUsers;

        public UserTests()
        {
            expectedUsers = new List<User>()
            {
                new User(1,"Howard","H.P. Lovecraft",1,"Lovecraft","Lovecraft@test.com",
                         "Lovecraft","098f6bcd4621d373cade4e832627b4f6",true,false, "FAKE_RESET_TOKEN",
                         "FAKE_CONFIRMATION_TOKEN","",DateTime.Now,DateTime.Now,DateTime.Now)
            };

            returnUsers = new List<User>()
            {
                new User(1,"Howard","H.P. Lovecraft",1,"Lovecraft","Lovecraft@test.com",
                         "Lovecraft","098f6bcd4621d373cade4e832627b4f6",true,false, "FAKE_RESET_TOKEN",
                         "FAKE_CONFIRMATION_TOKEN","",DateTime.Now,DateTime.Now,DateTime.Now)
            };

            IDataBaseFactory dataBaseFactory = new DataBaseHelper()
                                                   .Add<User>(returnUsers, 1)
                                                   .Build();

            userController = new UserController(
                                 new UserBusiness(
                                     new UserRepository(dataBaseFactory),
                                     new AppSettings() { Secret = "S21DAS2111F61HTBN6G51D6F8H61ZFTHS6E81FA" },
                                     EmailSenderHelper.Create()),
                                 LoggerHelper.Create<UserController>(),
                                 ValidatorHelper.Create(1, "FAKE_TOKEN"));

            userController.ControllerContext = HttpContextHelper.Create("FAKE_TOKEN");

        }

        [Fact]
        public void Login()
        {
            User input = new User()
            {
                Email = "Lovecraft@test.com",
                PasswordHash = "098f6bcd4621d373cade4e832627b4f6"
            };

            var result = userController.Login(input);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<User>>(result);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, (result.Result as OkObjectResult).StatusCode);
            Assert.Equal(expectedUsers.First().Id, ((result.Result as OkObjectResult).Value as User).Id);

        }

        [Fact]
        public void Find()
        {
            int input = 1;
            
            var result = userController.Find(input);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<User>>(result);
            Assert.Equal(200, (result.Result as OkObjectResult).StatusCode);
            Assert.Equal(expectedUsers.First().Id, ((result.Result as OkObjectResult).Value as User).Id);

        }

        [Fact]
        public void EmailConfirm()
        {
            string inputEmail = "Lovecraft@test.com";
            string inputToken = "FAKE_CONFIRMATION_TOKEN";

            var result = userController.EmailConfirm(inputEmail, inputToken);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
            Assert.Equal(200, (result as OkResult).StatusCode);

        }

        [Fact]
        public void PasswordChange()
        {
            int inputId = 1;
            string inputOldPassword = "098f6bcd4621d373cade4e832627b4f6";
            string inputNewPassword = "300c6bcd4621d37345814i832616a3c6";

            var result = userController.PasswordChange(inputId, inputOldPassword, inputNewPassword);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
            Assert.Equal(200, (result as OkResult).StatusCode);

        }

        [Fact]
        public void Create()
        {
            User input = new User(1, "Howard", "H.P. Lovecraft", 1, "Lovecraft", "Lovecraft@test.com",
                                 "Lovecraft", "098f6bcd4621d373cade4e832627b4f6", true, false, "FAKE_RESET_TOKEN",
                                 "FAKE_CONFIRMATION_TOKEN", "", DateTime.Now, DateTime.Now, DateTime.Now);

            var result = userController.Create(input);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
            Assert.Equal(200, (result as OkResult).StatusCode);

        }

        [Fact]
        public void Update()
        {
            User input = new User(1, "Howard", "H.P. Lovecraft", 1, "Lovecraft", "Lovecraft@test.com",
                                 "Lovecraft", "098f6bcd4621d373cade4e832627b4f6", true, false, "FAKE_RESET_TOKEN",
                                 "FAKE_CONFIRMATION_TOKEN", "", DateTime.Now, DateTime.Now, DateTime.Now);

            var result = userController.Update(input);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
            Assert.Equal(200, (result as OkResult).StatusCode);

        }

        [Fact]
        public void Delete()
        {
            User input = new User(1, "Howard", "H.P. Lovecraft", 1, "Lovecraft", "Lovecraft@test.com",
                                 "Lovecraft", "098f6bcd4621d373cade4e832627b4f6", true, false, "FAKE_RESET_TOKEN",
                                 "FAKE_CONFIRMATION_TOKEN", "", DateTime.Now, DateTime.Now, DateTime.Now);

            var result = userController.Delete(input);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
            Assert.Equal(200, (result as OkResult).StatusCode);

        }
    }
}
