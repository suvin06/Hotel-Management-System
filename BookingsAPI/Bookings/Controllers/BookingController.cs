using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookings.Models;
using Bookings.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Controllers
{
    
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class BookingsController : ControllerBase
    {
        ibooking idb;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BookingsController));
        public BookingsController(ibooking _idb)
        {
            idb = _idb;
        }

        [HttpGet]
        [Route("api/Booking/GetBooking")]
        public IActionResult GetBooking()
        {
            _log4net.Info("BookingsController Http GET ALL BOOKING");
            try
            {
                var room = idb.GetBooking();
                if (room != null)
                {
                    return Ok(room);
                }

                return NotFound();

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/Booking/PostBooking")]
        public IActionResult AddDetail([FromBody]Booking booking)
        {
            _log4net.Info("BookingsController Http ADD TO BOOKING");
            if (ModelState.IsValid)
            {
                try
                {
                    var Id = idb.PostBooking(booking);
                    if (Id > 0)
                    {
                        return Ok(Id);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("api/Booking/DeleteBooking")]
        public IActionResult DeleteDetail(int Id)
        {
            _log4net.Info("BookingsController Http DELETE FROM BOOKING");


            if (Id == null)
            {
                return BadRequest(Id);
            }

            try
            {
                var result = idb.DeleteBooking(Id);
                if (result == 0)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {

                return BadRequest(Id);
            }
        }
       
    }
}
