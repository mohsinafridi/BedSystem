using HotelCloudBedSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HotelCloudBedSystem.Data
{
    public class EFRepository : IEFRepository
    {
        private HotelCloudDbContext _context;

        public EFRepository(HotelCloudDbContext context)
        {
            _context = context;
        }

        #region Generics
        public void Add(object entity)
        {
            _context.Add(entity);
        }

        public void Update(object entity)
        {
            _context.Update(entity);
        }

        public void Delete(object entity)
        {
            _context.Remove(entity);
        }

        public bool SaveChange()
        {
            return _context.SaveChanges() > 0;
        }


        #region Hotel
        public PagedList<Hotel> Hotels(QueryOptions options, int Hotelid, bool IncludeRole = false)
        {

            if (IncludeRole == true && Hotelid != 0)
            {


                return new PagedList<Hotel>(
                    _context.hotels
                    .Include(p => p.AppUser)
                    .Where(p => p.HotelId == Hotelid)
                    , options);
            }

            if (IncludeRole == true && Hotelid == 0)
            {


                return new PagedList<Hotel>(
                    _context.hotels
                    .Include(p => p.AppUser)
                    , options);
            }


            return new PagedList<Hotel>(_context.hotels, options);

        }
        #endregion

        public IEnumerable<Hotel> GetHotels()
        {
            return _context.hotels;

        }

        public IEnumerable<HotelFloors> GetFloors()
        {
            return _context.hotelFloors;

        }


        public Hotel GetHotelById(int Hotelid, bool IncludeDetail = false)
        {
            if (IncludeDetail == true && Hotelid != 0)
            {
                return _context.hotels
                    .Include(p => p.AppUser)
                    .FirstOrDefault(p => p.HotelId == Hotelid);
            }
            return _context.hotels
                .FirstOrDefault(p => p.HotelId == Hotelid);
        }

        public Hotel HotelById(int id)
        {
            return _context.hotels
                            .FirstOrDefault(p => p.HotelId == id);
        }



        public Hotel GetHotelByManagerId(string ManagerId, bool IncludeDetail = false)
        {
            if (IncludeDetail == true && ManagerId != null)
            {
                return _context.hotels
                    .Include(p => p.AppUser)
                    .FirstOrDefault(p => p.AppUser.Id == ManagerId);
            }
            return _context.hotels
                     .FirstOrDefault(p => p.AppUser.Id == ManagerId);
        }
        #endregion

        #region Hotel CountsArea
        public int FloorsCount(int hotelId)
        {
            return _context.hotelFloors
                .Where(p => p.Hotel.HotelId == hotelId)
                .Count();
        }

        public int HotelRoomsCount(int hotelId)
        {
            return _context.hotelRooms
                .Where(p => p.Hotel.HotelId == hotelId)
                .Count();
        }

        public int FloorsRoomsCount(int floorId)
        {
            return _context.hotelRooms
                 .Where(p => p.HotelFloors.HotelFloorsId == floorId)
                .Count();
        }

        public int HotelBookedRoom(int hotelId)
        {
            return _context.hotelRooms.
                Where(p => p.IsBooked == true)
                .Where(p => p.Hotel.HotelId == hotelId)
                .Count();
        }

        public int FloorBookedRoom(int floorId)
        {
            return _context.hotelRooms.
                Where(p => p.IsBooked == true)
                .Where(p => p.HotelFloors.HotelFloorsId == floorId)
                .Count();
        }
        public int HotelNotBooked(int hotelId)
        {
            return _context.hotelRooms
                .Where(p => p.IsBooked == false)
                .Where(p => p.Hotel.HotelId == hotelId)
                .Count();
        }

        public int FloorNotBooked(int floorId)
        {
            return _context.hotelRooms
                .Where(p => p.IsBooked == false)
                .Where(p => p.HotelFloors.HotelFloorsId == floorId)
                .Count();
        }

        public int HotelTotalPaymentInNumbers(int hotelId)
        {
            return _context.payments
                .Where(p => p.HotelRoom.Hotel.HotelId == hotelId)
                .Count();
        }

        public int RoomsTotalPaymentInNumbers(int roomId)
        {
            return _context.payments
                .Where(p => p.HotelRoom.HotelRoomId == roomId)
                .Count();
        }

        public int HotelReservationCount(int hotelId)
        {
            return _context.roomReservations
                .Where(p => p.Hotelroom.Hotel.HotelId == hotelId)
                .Count();
        }

        public int RoomReservationCount(int roomId)
        {
            return _context.roomReservations
                .Where(p => p.Hotelroom.HotelRoomId == roomId)
                .Count();
        }
        #endregion

        #region floors
        public PagedList<HotelFloors> FloorsByHotelId(QueryOptions options, int Hotelid,
            int floorId, bool IncludeRole = false)
        {

            if (IncludeRole == true && Hotelid > 0 && floorId > 0)
            {
                return new PagedList<HotelFloors>(
                    _context.hotelFloors
                    .Include(p => p.Hotel)
                    .Where(p => p.Hotel.HotelId == Hotelid && p.HotelFloorsId == floorId)
                    , options);
            }

            if (IncludeRole == true && Hotelid > 0 && floorId == 0)
            {


                return new PagedList<HotelFloors>(
                    _context.hotelFloors
                    .Include(p => p.Hotel)
                    .Where(p => p.Hotel.HotelId == Hotelid)
                    , options);
            }




            return new PagedList<HotelFloors>(_context.hotelFloors, options);

        }
        #endregion

        #region Rooms
        public PagedList<HotelRoom> RoomsByHotelId(QueryOptions options, int Hotelid,
            int RoomId, int floorId, bool IncludeRole = false)
        {

            if (IncludeRole == true && Hotelid > 0 && floorId > 0 && RoomId > 0)
            {
                return new PagedList<HotelRoom>(
                    _context.hotelRooms
                    .Include(p => p.Hotel)
                    .Include(p => p.HotelFloors)
                    .Include(p=> p.HotelRoomType)
                    .Where(p => p.Hotel.HotelId == Hotelid &&
                    p.HotelFloors.HotelFloorsId == floorId &&
                    p.HotelRoomId == RoomId)
                    , options);
            }

            if (IncludeRole == true && Hotelid > 0 && floorId == 0 && RoomId > 0)
            {


                return new PagedList<HotelRoom>(
                    _context.hotelRooms
                    .Include(p => p.Hotel)
                    .Include(p => p.HotelFloors)
                    .Include(p => p.HotelRoomType)
                    .Where(p => p.Hotel.HotelId == Hotelid &&
                    p.HotelRoomId == RoomId)
                    , options);
            }

            if (IncludeRole == true && Hotelid > 0 && floorId > 0 && RoomId == 0)
            {


                return new PagedList<HotelRoom>(
                    _context.hotelRooms
                    .Include(p => p.Hotel)
                    .Include(p => p.HotelFloors)
                    .Include(p => p.HotelRoomType)
                    .Where(p => p.Hotel.HotelId == Hotelid &&
                    p.HotelFloors.HotelFloorsId == floorId)
                    , options);
            }




            return new PagedList<HotelRoom>(_context.hotelRooms.Include(p=>p.HotelRoomType), options);

        }
        #endregion


        #region Booked And Unbooked Rooms
        public PagedList<HotelRoom> BookedRoomsByHotelId(QueryOptions options, int Hotelid,
           int RoomId, int floorId, bool IncludeRole = false)
        {

            if (IncludeRole == true && Hotelid > 0 && floorId > 0 && RoomId > 0)
            {
                return new PagedList<HotelRoom>(
                    _context.hotelRooms
                    .Include(p => p.Hotel)
                    .Include(p => p.HotelFloors)
                    .Include(p => p.HotelRoomType)
                    .Where(p => p.Hotel.HotelId == Hotelid &&
                    p.HotelFloors.HotelFloorsId == floorId &&
                    p.HotelRoomId == RoomId && p.IsBooked == true)
                    , options);
            }

            if (IncludeRole == true && Hotelid > 0 && floorId == 0 && RoomId > 0)
            {


                return new PagedList<HotelRoom>(
                    _context.hotelRooms
                    .Include(p => p.Hotel)
                    .Include(p => p.HotelFloors)
                    .Include(p => p.HotelRoomType)
                    .Where(p => p.Hotel.HotelId == Hotelid &&
                    p.HotelRoomId == RoomId && p.IsBooked == true)
                    , options);
            }

            if (IncludeRole == true && Hotelid > 0 && floorId > 0 && RoomId == 0)
            {


                return new PagedList<HotelRoom>(
                    _context.hotelRooms
                    .Include(p => p.Hotel)
                    .Include(p => p.HotelFloors)
                    .Include(p => p.HotelRoomType)
                    .Where(p => p.Hotel.HotelId == Hotelid &&
                    p.HotelFloors.HotelFloorsId == floorId && p.IsBooked == true)
                    , options);
            }

            if (IncludeRole == true && Hotelid > 0 && floorId == 0 && RoomId == 0)
            {


                return new PagedList<HotelRoom>(
                    _context.hotelRooms
                    .Include(p => p.Hotel)
                    .Include(p => p.HotelFloors)
                    .Include(p => p.HotelRoomType)
                    .Where(p => p.Hotel.HotelId == Hotelid &&
                      p.IsBooked == true)
                    , options);
            }




            return new PagedList<HotelRoom>(_context.hotelRooms, options);
        }


        public PagedList<HotelRoom> UnBookedRoomsByHotelId(QueryOptions options, int Hotelid,
           int RoomId, int floorId, bool IncludeRole = false)
        {

            if (IncludeRole == true && Hotelid > 0 && floorId > 0 && RoomId > 0)
            {
                return new PagedList<HotelRoom>(
                    _context.hotelRooms
                    .Include(p => p.Hotel)
                    .Include(p => p.HotelFloors)
                    .Include(p => p.HotelRoomType)
                    .Where(p => p.Hotel.HotelId == Hotelid &&
                    p.HotelFloors.HotelFloorsId == floorId &&
                    p.HotelRoomId == RoomId && p.IsBooked == false)
                    , options);
            }

            if (IncludeRole == true && Hotelid > 0 && floorId == 0 && RoomId > 0)
            {


                return new PagedList<HotelRoom>(
                    _context.hotelRooms
                    .Include(p => p.Hotel)
                    .Include(p => p.HotelFloors)
                    .Include(p => p.HotelRoomType)
                    .Where(p => p.Hotel.HotelId == Hotelid &&
                    p.HotelRoomId == RoomId && p.IsBooked == false)
                    , options);
            }

            if (IncludeRole == true && Hotelid > 0 && floorId > 0 && RoomId == 0)
            {


                return new PagedList<HotelRoom>(
                    _context.hotelRooms
                    .Include(p => p.Hotel)
                    .Include(p => p.HotelFloors)
                    .Include(p => p.HotelRoomType)
                    .Where(p => p.Hotel.HotelId == Hotelid &&
                    p.HotelFloors.HotelFloorsId == floorId && p.IsBooked == false)
                    , options);
            }


            if (IncludeRole == true && Hotelid > 0 && floorId == 0 && RoomId == 0)
            {


                return new PagedList<HotelRoom>(
                    _context.hotelRooms
                    .Include(p => p.Hotel)
                    .Include(p => p.HotelRoomType)
                    .Include(p => p.HotelFloors)
                    .Where(p => p.Hotel.HotelId == Hotelid &&
                     p.IsBooked == false)
                    , options);
            }




            return new PagedList<HotelRoom>(_context.hotelRooms, options);
        }
        #endregion


        #region reservatiobn Histry

        public PagedList<RoomReservation> ReservationHistory(QueryOptions options, int Hotelid,
     int RoomId, int floorId, int reservationId, bool IncludeRole = false)
        {

            if (IncludeRole == true && Hotelid > 0 && floorId > 0 && RoomId > 0 && reservationId > 0)
            {
                return new PagedList<RoomReservation>(
                    _context.roomReservations
                    .Include(p => p.Hotelroom.Hotel)
                    .Include(p => p.Hotelroom)
                    .Include(p => p.Hotelroom.HotelFloors)
                    .Where(p => p.Hotelroom.Hotel.HotelId == Hotelid ||
                    p.Hotelroom.HotelFloors.HotelFloorsId == floorId ||
                    p.Hotelroom.HotelRoomId == RoomId || p.RoomReservationId == reservationId)
                    , options); ;
            }



            return new PagedList<RoomReservation>(_context.roomReservations, options);
        }

        #endregion


        #region Floors And Rooms
        public IEnumerable<HotelFloors> HotelFloors(int hotelId, bool includeDetail = false)
        {
            if (includeDetail)
            {
                return _context.hotelFloors
                    .Include(p => p.Hotel)
                    .Where(p => p.Hotel.HotelId == hotelId);
            }
            return _context.hotelFloors;
        }

        public IEnumerable<HotelRoom> HotelRooms(int hotelId, bool includeDetail = false)
        {
            if (includeDetail)
            {
                return _context.hotelRooms
                    .Include(p => p.Hotel)
                    .Include(p=> p.HotelFloors)
                    .Where(p => p.Hotel.HotelId == hotelId);
            }
            return _context.hotelRooms;
        }
        #endregion


        public HotelRoom RoomById(int id)
        {
            return _context.hotelRooms
                .Include(p=> p.HotelRoomType)
                .FirstOrDefault(p => p.HotelRoomId == id);
        }

        public HotelFloors FloorById(int id)
        {
            return _context.hotelFloors
                .FirstOrDefault(p => p.HotelFloorsId == id);
        }

        public IEnumerable<HotelFloors> getHotelFloorsbyHotelId(int hotelId)
        {
            return _context.hotelFloors
                .Where(p => p.Hotel.HotelId == hotelId);
        }



        public Hotel GetHotelByUserId(string Userid, bool IncludeDetail = false)
        {
            if (IncludeDetail == true && Userid != null)
            {
                return _context.hotels
                    .Include(p => p.AppUser)
                    .FirstOrDefault(p => p.AppUser.Id == Userid);
            }
            return _context.hotels
                .FirstOrDefault(p => p.AppUser.Id == Userid);
        }




    }

}
