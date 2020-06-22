using Microsoft.AspNetCore.Http;

namespace HotelCloudBedSystem.Areas.Manager.ViewModels
{
    public class HotelRoomImagesViewModel
    {
        public byte[] HotelRoomimage { get; set; }
        public IFormFile hotelRoomimage { get; set; }
        public int HotelRoomId { get; set; }
    }
}
