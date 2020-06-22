using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Models
{
    [Table("Payment")]
    public class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        public string  PaymentType { get; set; }

        public RoomReservation  RoomReservation { get; set; }

        public HotelRoom  HotelRoom { get; set; }
        public int Amount { get; set; }


    }
}
