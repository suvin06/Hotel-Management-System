using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSys.Models
{
    public partial class Booking
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string RoomType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int AdhaarCardNo { get; set; }
    }
}
