using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewModels
{
    public class PaymentViewModel
    {
        public DateTime ChkIndate { get; set; }
        public DateTime ChkOutdate { get; set; }
        public string UserName { get; set; }
        public string PhoneNo { get; set; }
        public int ZipCode { get; set; }
        public string Cnic { get; set; }
        public string UserCity { get; set; }
        public string Email { get; set; }
        public string HotelName { get; set; }
        public string Address { get; set; }
        public int RoomId { get; set; }
        public string Roomname { get; set; }
        public int Price { get; set; }
        public int HotelId { get; set; }
        public string HotelCity { get; set; }
        public int HotelStar { get; set; }
        public string hotelName { get; set; }
        public string HotelAddress { get; set; }
        public byte[] HotelImage { get; set; }
        public int TotalPrice { get; set; }
        public int NoOfDays { get; set; }

        public int ReservationId { get; set; }
        public int RoomNo { get; set; }
    }
}
