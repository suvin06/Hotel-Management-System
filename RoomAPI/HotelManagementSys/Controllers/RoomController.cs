using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSys.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSys.Controllers
{
    
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class RoomController : ControllerBase
    {
        iroomtype rt;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(RoomController));
        public RoomController(iroomtype _rt)
        {
            rt = _rt;
        }


        [HttpGet]
        [Route("api/Room/GetRoomType")]

        public IActionResult GetRoomType()
        {
            _log4net.Info("RoomController Http GET ALL ROOMTYPE");
            try
            {
                var rooms = rt.GetRoomType();
                if (rooms != null)
                {
                    return Ok(rooms);
                }

                return NotFound();

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/Room/GetBooking")]
        public IActionResult GetBooking()
        {
            _log4net.Info("RoomController Http GET ALL BOOKINGS");
            try
            {
                var room = rt.GetBooking();
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

    }
}
