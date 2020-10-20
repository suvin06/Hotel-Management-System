using HotelManagementSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSys.Repository
{
    public interface iroomtype
    {
       List<RoomType> GetRoomType();

       List<Booking> GetBooking();
    }

}
