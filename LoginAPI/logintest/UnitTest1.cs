
using login.Controllers;
using login.Models;
using login.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using MVCController.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace logintest
{
    public class Tests
    {
        hoteldbContext hdb;

        ilogin login;

        

        List<LoginDetails> user = new List<LoginDetails>();
            IQueryable<LoginDetails> userdata;
            Mock<DbSet<LoginDetails>> mockSet;
            Mock<hoteldbContext> usercontextmock;


        
        [SetUp]
        public void Setup1()
        {
           var user = new List<LoginDetails>()
            {
                new LoginDetails{DetailId=1,UserName="abc",Password="abc123"},
                new LoginDetails{DetailId=2,UserName="cde",Password="cde123"},
                new LoginDetails{DetailId=3,UserName="xyz",Password="xyz123"}

            };
            userdata = user.AsQueryable();
            mockSet = new Mock<DbSet<LoginDetails>>();
            mockSet.As<IQueryable<LoginDetails>>().Setup(m => m.Provider).Returns(userdata.Provider);
            mockSet.As<IQueryable<LoginDetails>>().Setup(m => m.Expression).Returns(userdata.Expression);
            mockSet.As<IQueryable<LoginDetails>>().Setup(m => m.ElementType).Returns(userdata.ElementType);
            mockSet.As<IQueryable<LoginDetails>>().Setup(m => m.GetEnumerator()).Returns(userdata.GetEnumerator());
            var p = new DbContextOptions<hoteldbContext>();
            usercontextmock = new Mock<hoteldbContext>(p);
            usercontextmock.Setup(x => x.LoginDetails).Returns(mockSet.Object);



        }
        [Test]
        public void LoginTest1()
        {

            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("ThisismySecretKey");
            var controller = new TokenValidateController(config.Object, usercontextmock.Object);
            var auth = controller.LoginResult(new User { UserName = "abc", Password = "abc123" }) as OkObjectResult;

            Assert.AreEqual(200, auth.StatusCode);

        }

        

        [Test]
        public void LoginTestFail()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("ThisismySecretKey");
            var controller = new TokenValidateController(config.Object, usercontextmock.Object);
            var auth = controller.LoginResult(new User { UserName = "abc", Password = "c123" }) as OkObjectResult;
            Assert.IsNull(auth);

        }




    }
}
  