namespace HotelCloudBedSystem.Areas.Manager.ViewModels
{
    public class HotelRoomListViewModel
    {
        public int RoomId { get; set; }
        public int OccupancyLimit { get; set; }
        public string RoomTye { get; set; }
        public string  Description { get; set; }
        public bool Isbooked { get; set; }
        public string  RoomName { get; set; }
        public int RoomNo { get; set; }
        public byte[]  RoomImage { get; set; }
        public int PerNightPrice { get; set; }
        public bool Tv { get; set; }
        public bool Ac { get; set; }
        public bool FreeWifi { get; set; }
        public bool AttachedWashroom { get; set; }
    }
}
