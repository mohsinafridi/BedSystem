using HotelCloudBedSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HotelCloudBedSystem.Filteration.CheckOutCheckIn
{
    public class CheckOutCheckInImplmentation : ICheckOutCheckInImplmentation
    {
        private HotelCloudDbContext _context;

        public CheckOutCheckInImplmentation(HotelCloudDbContext context)
        {
            _context = context;
        }

        public bool Check(int roomId, DateTime CheckIn, DateTime CheckOut)
        {
            int ReservedCount = 0;

            var Roomreservation = _context.roomReservations.
                        Include(p => p.Hotelroom)
                        .FirstOrDefault(p => p.Hotelroom.HotelRoomId
                        == roomId);
            TimeSpan chkoutDifference = CheckOut - Roomreservation.ChkOutdate.Date;

            if (chkoutDifference.Days > 0)
            {
                TimeSpan chkinDifference = CheckIn - Roomreservation.ChkOutdate.Date;

                if (chkinDifference.Days > 0)
                {
                    ReservedCount++;
                }
            }

            if (ReservedCount == 0)
            {
                return false;
            }
            return true;
        }
    }
}
