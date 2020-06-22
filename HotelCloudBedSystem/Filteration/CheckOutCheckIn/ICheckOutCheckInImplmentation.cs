using System;

namespace HotelCloudBedSystem.Filteration.CheckOutCheckIn
{
    public interface ICheckOutCheckInImplmentation
    {
        bool Check(int roomId, DateTime CheckIn, DateTime CheckOut);
    }
}