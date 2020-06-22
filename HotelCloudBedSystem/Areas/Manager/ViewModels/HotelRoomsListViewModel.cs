using HotelCloudBedSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelCloudBedSystem.Areas.Manager.ViewModels
{
    public class HotelRoomsListViewModel
    {
        public int HotelRoomId { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public HotelFloors HotelFloors { get; set; }
        public Hotel Hotel { get; set; }
        public int OcupancyLimit { get; set; }
        public bool IsBooked { get; set; }
    }
}
