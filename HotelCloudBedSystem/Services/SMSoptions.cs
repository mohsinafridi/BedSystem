using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Services
{
    public class SMSoptions
    {
        public string SMSAccountIdentification { get; set; }
        public string SMSAccountPassword { get; set; }
        public string SMSAccountFrom { get; set; }

        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string SendNumber { get; set; }
    }
}
