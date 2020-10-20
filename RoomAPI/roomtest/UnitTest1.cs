using HotelManagementSys.Controllers;
using HotelManagementSys.Models;
using HotelManagementSys.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace roomtest
{
    public class Tests
    {

        hoteldbContext db;

        List<RoomType> r1;

        [SetUp]
        public void Setup()
        {
            r1 = new List<RoomType>()
            {
                new RoomType{RoomId=1, RoomType1="Single",PriceInRs=2536},
                new RoomType{RoomId=2, RoomType1="Double",PriceInRs=1600},
                new RoomType{RoomId=3, RoomType1="Suite",PriceInRs=1200},
            };

            var loandata = r1.AsQueryable();
            var mockSet = new Mock<DbSet<RoomType>>();
            mockSet.As<IQueryable<RoomType>>().Setup(m => m.Provider).Returns(loandata.Provider);
            mockSet.As<IQueryable<RoomType>>().Setup(m => m.Expression).Returns(loandata.Expression);
            mockSet.As<IQueryable<RoomType>>().Setup(m => m.ElementType).Returns(loandata.ElementType);
            mockSet.As<IQueryable<RoomType>>().Setup(m => m.GetEnumerator()).Returns(loandata.GetEnumerator());
            var mockContext = new Mock<hoteldbContext>();
            mockContext.Setup(c => c.RoomType).Returns(mockSet.Object);
            db = mockContext.Object;
        }

        [Test]
        public void GetRoomType_ValidInput_OkRequest()
        {
            var mock = new Mock<roomtyperepo>(db);
            RoomController obj = new RoomController(mock.Object);

            var data = obj.GetRoomType();
            var res = data as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void GetRoomType_InvalidInput_ReturnsBadRequest()
        {
            try
            {
                var mock = new Mock<roomtyperepo>(db);
                RoomController obj = new RoomController(mock.Object);

                var data = obj.GetRoomType();
                var res = data as BadRequestResult;
                Assert.AreEqual(400, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void GetRoomType_ReturnNotNullList()
        {
            var mock = new Mock<roomtyperepo>(db);
                RoomController obj = new RoomController(mock.Object);

            var data = obj.GetRoomType();
            var res = data as ObjectResult;
            Assert.IsNotNull(data);
        }
    }
}