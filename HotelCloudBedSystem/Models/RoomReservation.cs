using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace HotelCloudBedSystem.Models
{
    [Table("RoomReservation")]
    public class RoomReservation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomReservationId { get; set; }

        public DateTime ChkIndate { get; set; }

        public DateTime ChkOutdate { get; set; }

        public HotelRoom Hotelroom { get; set; }
        public string UserName { get; set; }
        public string  Email { get; set; }
        public string  PhoneNo { get; set; }
        public string  Cnic { get; set; }
        public string  Address { get; set; }

        public string  UserCity { get; set; }
        public int ZipCode { get; set; }
        public int NoOfNight { get; set; }
        public bool IsPaymentSuccessfull { get; set; }
        public bool IsActive { get; set; }



    }
}
