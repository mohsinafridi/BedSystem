using HotelCloudBedSystem.Models;
using System.Collections.Generic;

namespace HotelCloudBedSystem.Data
{
    public interface IEFRepository
    {
        void Add(object entity);
        PagedList<HotelRoom> BookedRoomsByHotelId(QueryOptions options, int Hotelid, int RoomId, int floorId, bool IncludeRole = false);
        void Delete(object entity);
        int FloorBookedRoom(int floorId);
        HotelFloors FloorById(int id);
        int FloorNotBooked(int floorId);
        PagedList<HotelFloors> FloorsByHotelId(QueryOptions options, int Hotelid, int floorId, bool IncludeRole = false);
        int FloorsCount(int hotelId);
        int FloorsRoomsCount(int floorId);
        IEnumerable<HotelFloors> GetFloors();
        Hotel GetHotelById(int Hotelid, bool IncludeDetail = false);
        Hotel GetHotelByManagerId(string ManagerId, bool IncludeDetail = false);
        Hotel GetHotelByUserId(string Userid, bool IncludeDetail = false);
        IEnumerable<HotelFloors> getHotelFloorsbyHotelId(int hotelId);
        IEnumerable<Hotel> GetHotels();
        int HotelBookedRoom(int hotelId);
        Hotel HotelById(int id);
        IEnumerable<HotelFloors> HotelFloors(int hotelId, bool includeDetail = false);
        int HotelNotBooked(int hotelId);
        int HotelReservationCount(int hotelId);
        IEnumerable<HotelRoom> HotelRooms(int hotelId, bool includeDetail = false);
        int HotelRoomsCount(int hotelId);
        PagedList<Hotel> Hotels(QueryOptions options, int Hotelid, bool IncludeRole = false);
        int HotelTotalPaymentInNumbers(int hotelId);
        PagedList<RoomReservation> ReservationHistory(QueryOptions options, int Hotelid, int RoomId, int floorId, int reservationId, bool IncludeRole = false);
        HotelRoom RoomById(int id);
        int RoomReservationCount(int roomId);
        PagedList<HotelRoom> RoomsByHotelId(QueryOptions options, int Hotelid, int RoomId, int floorId, bool IncludeRole = false);
        int RoomsTotalPaymentInNumbers(int roomId);
        bool SaveChange();
        PagedList<HotelRoom> UnBookedRoomsByHotelId(QueryOptions options, int Hotelid, int RoomId, int floorId, bool IncludeRole = false);
        void Update(object entity);
    }
}