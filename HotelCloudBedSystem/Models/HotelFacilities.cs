using System.ComponentModel.DataAnnotations.Schema;

namespace HotelCloudBedSystem.Models
{
    [Table("HotelFacilities")]
    public class HotelFacilities
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelFacilitiesId { get; set; }
        public bool FreeWifi { get; set; }
        public bool  BreckFast { get; set; }
        public bool Lunch { get; set; }
        public bool AttachedWashrooms { get; set; }
        public bool Receptionservices { get; set; }
        public bool Dinner { get; set; }
        public bool CarParking { get; set; }
        public bool Laundry { get; set; }
        public Hotel  Hotel { get; set; }
    }
}
