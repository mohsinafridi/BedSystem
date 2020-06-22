using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Filteration.CheckOutCheckIn;
using HotelCloudBedSystem.Filteration.HotelFilteration;
using HotelCloudBedSystem.Models;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelCloudBedSystem.Controllers
{
    public class HotelSearchController : Controller
    {
        private HotelCloudDbContext _context;
        private IEFRepository _repository;
        private IFilterHotelByCityAndRoomType _filterHotelByCityAndRoomType;
        private IFilterHotelByPriceAndHotelRatings _filterHotelByPriceAndHotelRatings;
        private IFilterHotelByPrice _filterHotelByPrice;
        private IFilterHotelByHotelRatings _filterHotelByHotelRatings;
        private IHotelNetworkMainSearchEngine _hotelNetworkMainSearchEngine;
        private ICheckOutCheckInImplmentation _checkOutCheckInImplmentation;
        int AverageStar = 0;
        int Averageprice = 0;
        int TotalPrice = 0;
        int TotalStar = 0;
        public HotelSearchController(
            HotelCloudDbContext context,
            IEFRepository repository , 
            IFilterHotelByCityAndRoomType filterHotelByCityAndRoomType,
            IFilterHotelByPriceAndHotelRatings filterHotelByPriceAndHotelRatings,
            IFilterHotelByPrice filterHotelByPrice,
            IFilterHotelByHotelRatings filterHotelByHotelRatings,
            IHotelNetworkMainSearchEngine hotelNetworkMainSearchEngine,
            ICheckOutCheckInImplmentation checkOutCheckInImplmentation)
        {
            _context = context;
            _repository = repository;
            _filterHotelByCityAndRoomType = filterHotelByCityAndRoomType;
            _filterHotelByPriceAndHotelRatings = filterHotelByPriceAndHotelRatings;
            _filterHotelByPrice = filterHotelByPrice;
            _filterHotelByHotelRatings = filterHotelByHotelRatings;
            _hotelNetworkMainSearchEngine = hotelNetworkMainSearchEngine;
            _checkOutCheckInImplmentation = checkOutCheckInImplmentation;
        }
        public IActionResult Index()
        {
            return View();
        }


        #region HotelFiltering
        [HttpPost]
        public IActionResult Search(HotelSerachViewModel model)
        
        {
            DateTime TempchkIn = new DateTime();
            DateTime TempchkOut =new DateTime();
            var hotelList = new List<HotelListViewModel>();
            HotelListViewModel hotelModel = null;
            List<Hotel> SearchHotels = new List<Hotel>();
            HotelRoomType RoomType = new HotelRoomType();
            int RoomCount = 0;
           




            // filtered Hotel by Hotel City And RoomType
            if (model.City != null && model.RoomTypeId > 0 
                && model.checkOut ==null && model.checkIn ==null)
            {
                SearchHotels = _filterHotelByCityAndRoomType.
                    GetHotelByCityAndRoomType(model.City,model.checkIn,model.checkOut);
                RoomType = RoomTypeById(model.RoomTypeId);
            }

            // Filtered Hotel by Price and StarRating
            else if (model.Price >0 && model.StarRatingid > 0)
            {
                SearchHotels = _filterHotelByPriceAndHotelRatings.
                    GetHotelByPriceAndHotelRatings(model.City,model.Price,
                    model.StarRatingid ,model.checkIn ,model.checkOut);
                RoomType = RoomTypeById(model.hotelRoomTypeId);
            }
            // Filtered Hotel By HotelPrice
            else if (model.Price >0 && model.StarRatingid == 0)
            {
                SearchHotels = _filterHotelByPrice.
                    GetHotelByPrice(model.City,model.Price ,model.checkIn,model.checkOut);
                RoomType = RoomTypeById(model.hotelRoomTypeId);
            }
            //Filtered Hotel By StarRating

            else if(model.Price ==0 && model.StarRatingid > 0)
            {
                SearchHotels = _filterHotelByHotelRatings.
                    GetHotelByHotelRatings(model.City, model.StarRatingid 
                    ,model.checkIn,model.checkOut);
                RoomType = RoomTypeById(model.hotelRoomTypeId);
            }


              //Filtered Hotel By Main Search Engine

            else if(model.City != null && 
                model.checkIn != null && 
                model.checkOut != null && model.RoomTypeId > 0)
            {
                SearchHotels = _hotelNetworkMainSearchEngine.
                    GetHotelBySearchEngine(model.City,
                    model.checkIn, model.checkOut);
                RoomType = RoomTypeById(model.RoomTypeId);
                TempchkIn = model.checkIn;
                TempchkOut = model.checkOut;
            }

            foreach (var hotel in SearchHotels)
            {

                var Rooms = _context.hotelRooms
           .Where(p => p.Hotel.HotelId == hotel.HotelId)
           .Where(p => p.HotelRoomType.HotelRoomTypeId == RoomType.HotelRoomTypeId).ToList();

                if(Rooms != null)
                {
                    foreach(var room in Rooms)
                    {
                        if(room.IsBooked == false)
                        {
                            RoomCount++;
                        }
                        else if (room.IsBooked == true)
                        {

                            if (_checkOutCheckInImplmentation.
                                Check(room.HotelRoomId, model.checkIn, model.checkOut) == true)
                            {
                                RoomCount++;
                            }

                        }
                    }
                }

                var Averagereview = _context.hotelReviews.
                    Include(p => p.hotel).Where(p => p.hotel.HotelId == hotel.HotelId).ToList();
               
                if (Averagereview != null)
                {
                    for (int review = 0; review < Averagereview.Count; review++)
                    {
                        TotalStar = TotalStar + Averagereview[review].ReviewStar;
                    }

                    AverageStar = TotalStar / Averagereview.Count;
                }

                var RoomPrice = _context.hotelRooms.
                    Include(p => p.Hotel).Where(p => p.Hotel.HotelId == hotel.HotelId).ToList();

                if(RoomPrice != null)
                {
                    for (int price = 0; price < RoomPrice.Count; price++)
                    {
                        TotalPrice = TotalPrice + RoomPrice[price].RsPernight;
                    }

                    Averageprice = TotalPrice / (RoomPrice.Count);
                }
               

                if (RoomCount > 0)
                {
                    hotelModel = new HotelListViewModel()
                    {
                        HotelName = hotel.HotelName,
                        HotelCity = model.City,
                        NoOfRoomsFree = RoomCount,
                        HotelImage = hotel.HotelImage,
                        HotelId = hotel.HotelId,
                        ReviewStar = AverageStar,
                        AveragePrice = Averageprice,
                        TotalReview= Averagereview.Count,
                        hotelRoomtype = RoomType.RoomType,
                        HotelRoomTypeId= RoomType.HotelRoomTypeId,
                        

                    };

                    if(TempchkIn != null && TempchkOut != null)
                    {
                        hotelModel.CheckIn = TempchkIn;
                        hotelModel.CheckOut = TempchkOut;
                    }


                    hotelList.Add(hotelModel);
                    AverageStar = 0;
                    Averageprice = 0;
                    TotalPrice = 0;
                    TotalStar = 0;
                    RoomCount = 0;
                }



            }


            return View(hotelList);
        }


        #endregion  




        


      


        #region RoomTypeById

        public HotelRoomType RoomTypeById(int roomTypeId)
        {
            return  _context.hotelRoomTypes
                .FirstOrDefault(p => p.HotelRoomTypeId == roomTypeId);
        }

        #endregion
    }
}