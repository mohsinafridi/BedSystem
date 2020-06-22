using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelCloudBedSystem.Data
{
    public class HotelCloudDbContext: IdentityDbContext<AppUser, AppRole, string>
    {

        private IConfiguration _config;

        public HotelCloudDbContext(DbContextOptions<HotelCloudDbContext> options, IConfiguration config)
         : base(options)
        {
            _config = config;
        }

        public DbSet<AppUser> appUsers { get; set; }
        public DbSet<AppRole>  appRoles { get; set; }
        public DbSet<Hotel>  hotels { get; set; }
        public DbSet<HotelRoom>  hotelRooms { get; set; }
        public DbSet<HotelImages>  hotelImages { get; set; }
        public DbSet<RoomReservation>  roomReservations  { get; set; }
        public DbSet<RoomsImage>  roomsImages { get; set; }
        public DbSet<Payment>  payments { get; set; }
        public DbSet<HotelFloors>  hotelFloors { get; set; }
        public DbSet<City>  cities { get; set; }
        public DbSet<HotelRoomType>  hotelRoomTypes { get; set; }
        public DbSet<TemporaryRoomType>  temporaryRoomTypes { get; set; }
        public DbSet<HotelReview>  hotelReviews { get; set; }
        public DbSet<RoomFacilities>  RoomFacilities { get; set; }
        public DbSet<HotelFacilities>  HotelFacilities { get; set; }
        public DbSet<RoomReview>  roomReviews { get; set; }
        public DbSet<CityMapLocation>  cityMapLocations { get; set; }
        public DbSet<HotelMapLocation>  hotelMapLocations { get; set; }
        public DbSet<StarRating>  starRatings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:CloudeHotelConnectionString"]);
        }
    }
}
