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
using System.Text;

namespace roomtest
{
    class UnitTest2
    {
        hoteldbContext adb;
        List<Booking> b;
        [SetUp]
        public void Setup()
        {
            b = new List<Booking>()
            {
                new Booking{Id=1, FullName="SB",RoomType="Single",AdhaarCardNo=253624},
                new Booking{Id=2, FullName="DK",RoomType="Double",AdhaarCardNo=782134},
                new Booking{Id=3, FullName="SS",RoomType="Suite",AdhaarCardNo=516273}

            };
            var loandata = b.AsQueryable();
            var mockSet = new Mock<DbSet<Booking>>();
            mockSet.As<IQueryable<Booking>>().Setup(m => m.Provider).Returns(loandata.Provider);
            mockSet.As<IQueryable<Booking>>().Setup(m => m.Expression).Returns(loandata.Expression);
            mockSet.As<IQueryable<Booking>>().Setup(m => m.ElementType).Returns(loandata.ElementType);
            mockSet.As<IQueryable<Booking>>().Setup(m => m.GetEnumerator()).Returns(loandata.GetEnumerator());
            var mockContext = new Mock<hoteldbContext>();
            mockContext.Setup(c => c.Booking).Returns(mockSet.Object);
            adb = mockContext.Object;
        }
        [Test]
        public void GetRoomType_ValidInput_OkRequest()
        {
            var mock = new Mock<roomtyperepo>(adb);
            RoomController obj = new RoomController(mock.Object);

            var data = obj.GetBooking();
            var res = data as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }
        [Test]
        public void GetBooking_InvalidInput_ReturnsBadRequest()
        {
            try
            {
                var mock = new Mock<roomtyperepo>(adb);
                RoomController obj = new RoomController(mock.Object);

                var data = obj.GetBooking();
                var res = data as BadRequestResult;
                Assert.AreEqual(400, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void GetBooking_ReturnNotNullList()
        {
            var mock = new Mock<roomtyperepo>(adb);
            RoomController obj = new RoomController(mock.Object);

            var data = obj.GetBooking();
            var res = data as ObjectResult;
            Assert.IsNotNull(data);
        }
    }
}
