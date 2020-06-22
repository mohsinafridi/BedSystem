using System.ComponentModel.DataAnnotations.Schema;

namespace HotelCloudBedSystem.Models
{
    [Table("HotelMapLocation")]
    public class HotelMapLocation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelMapLocationId { get; set; }
        public string  HotelName { get; set; }
        public string  Latitude { get; set; }
        public string  Longitude { get; set; }
        public CityMapLocation  CityMapLocation { get; set; }
    }
}
