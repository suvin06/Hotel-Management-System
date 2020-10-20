using System;
using System.Collections.Generic;

namespace Bookings.Models
{
    public partial class RoomType
    {
        public int RoomId { get; set; }
        public string RoomType1 { get; set; }
        public decimal PriceInRs { get; set; }
    }
}
