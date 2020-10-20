using HotelManagementSys.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSys.Repository
{
    public class roomtyperepo : iroomtype
    {
        hoteldbContext hdb;
        public roomtyperepo(hoteldbContext _hdb)
        {
            hdb = _hdb;
        }

        public List<Booking> GetBooking()
        {
            if (hdb != null)
            {
                var post = hdb.Booking.ToList();
                if (post != null)
                {
                    return post;
                }
                return null;
            }
            else
                return null;
           
        }


        public List<RoomType> GetRoomType()
        {
            if (hdb != null)
            {
                return hdb.RoomType.ToList();
            }
            return null;
        }
    }
}
