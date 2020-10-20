using System;
using System.Collections.Generic;

namespace Bookings.Models
{
    public partial class LoginDetails
    {
        public int DetailId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
