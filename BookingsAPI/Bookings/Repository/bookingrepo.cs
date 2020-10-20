using Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookings.Repository
{
    public class bookingrepo : ibooking
    {
        hoteldbContext db;
        public bookingrepo(hoteldbContext _db)
        {
            db = _db;
        }
        public int DeleteBooking(int id)
        {
            int result = 0;

            if (db != null)
            {

                var post = db.Booking.FirstOrDefault(x => x.Id == id);

                if (post != null)
                {

                    db.Booking.Remove(post);
                    result = db.SaveChanges();
                    return 1;
                }
                return result;
            }
            return result;
        }

        public List<Booking> GetBooking()
        {
            if (db != null)
            {
                var post = db.Booking.ToList();
                if (post != null)
                {
                    return post;
                }
                return null;
            }
            else
                return null;
        }

        public int PostBooking(Booking data)
        {
            if (db != null)
            {
                db.Booking.Add(data);
                db.SaveChanges();

                return data.Id;
            }

            return 0;
        }

       
    }
}
