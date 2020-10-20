using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookings.Models;

namespace Bookings.Repository
{
    public interface ibooking
    {
        List<Booking> GetBooking();
        int PostBooking(Booking data);

        
        int DeleteBooking(int id);
        

    }
}
