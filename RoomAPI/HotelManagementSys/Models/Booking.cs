using System;
using System.Collections.Generic;

namespace HotelManagementSys.Models
{
    public partial class Booking
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string RoomType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int AdhaarCardNo { get; set; }
    }
}
