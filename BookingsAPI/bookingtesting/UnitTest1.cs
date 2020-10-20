using Bookings.Controllers;
using Bookings.Models;
using Bookings.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bookingtesting
{
    public class Tests
    {
        hoteldbContext db;
        

        List<Booking> b1;

        [SetUp]
        public void Setup()
        {
            b1 = new List<Booking>()
            {
                new Booking{Id=1, FullName="SB",RoomType="Single",AdhaarCardNo=253624},
                new Booking{Id=2, FullName="DK",RoomType="Double",AdhaarCardNo=782134},
                new Booking{Id=3, FullName="SS",RoomType="Suite",AdhaarCardNo=516273}

            };

            var loandata = b1.AsQueryable();
            var mockSet = new Mock<DbSet<Booking>>();
            mockSet.As<IQueryable<Booking>>().Setup(m => m.Provider).Returns(loandata.Provider);
            mockSet.As<IQueryable<Booking>>().Setup(m => m.Expression).Returns(loandata.Expression);
            mockSet.As<IQueryable<Booking>>().Setup(m => m.ElementType).Returns(loandata.ElementType);
            mockSet.As<IQueryable<Booking>>().Setup(m => m.GetEnumerator()).Returns(loandata.GetEnumerator());
            var mockContext = new Mock<hoteldbContext>();
            mockContext.Setup(c => c.Booking).Returns(mockSet.Object);
            db = mockContext.Object;
        }
       

        [Test]
        public void GetBooking_ValidInput_OkRequest()
        {
            var mock = new Mock<bookingrepo>(db);
           BookingsController obj = new BookingsController(mock.Object);
            
            var data = obj.GetBooking();
            var res = data as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void GetBooking_InvalidInput_ReturnsBadRequest()
        {
            try
            {
                var mock = new Mock<bookingrepo>(db);
                BookingsController obj = new BookingsController(mock.Object);

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
            var mock = new Mock<bookingrepo>(db);
            BookingsController obj = new BookingsController(mock.Object);

            var data = obj.GetBooking();
            var res = data as ObjectResult;
            Assert.IsNotNull(data);
        }

        [Test]
        public void AddDetail_ValidInput_OkRequest()
        {
            var mock = new Mock<bookingrepo>(db);
            BookingsController obj = new BookingsController(mock.Object);
            Booking book = new Booking { Id = 1, FullName = "SB", RoomType = "Single", AdhaarCardNo = 253624 };
            var data = obj.AddDetail(book);
            var res = data as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void AddDetail_InvalidInput_BadRequest()
        {
            try
            {
                var mock = new Mock<bookingrepo>(db);
                BookingsController obj = new BookingsController(mock.Object);
                Booking book = new Booking { Id = 1, FullName = "SB", RoomType = "Single", AdhaarCardNo = 253624 };
                var data = obj.AddDetail(book);
                var res = data as BadRequestObjectResult;
                Assert.AreEqual(400, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void DeleteDetail_ValidInput_OkRequest()
        {
            var mock = new Mock<bookingrepo>(db);
            BookingsController obj = new BookingsController(mock.Object);
            
            var data = obj.DeleteDetail(1);
            var res = data as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void DeleteDetail_InvalidInput_BadRequest()
        {
            try
            {
                var mock = new Mock<bookingrepo>(db);
                BookingsController obj = new BookingsController(mock.Object);
                
                var data = obj.DeleteDetail(0);
                var res = data as BadRequestObjectResult;
                Assert.AreEqual(400, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void DeleteDetail_ReturnNotNullList()
        {
            var mock = new Mock<bookingrepo>(db);
            BookingsController obj = new BookingsController(mock.Object);

            var data = obj.DeleteDetail(1);
            var res = data as ObjectResult;
            Assert.IsNotNull(data);
        }

    }
}