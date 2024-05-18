namespace HotelBookingBlazorApp.Data.Entities
{
    public class RoomTypeAmenity
    {
        public int RoomTypeId { get; set; }
        public int AmenityId { get; set; }
        public int? Unit { get; set; }

        public virtual Room Room { get; set; }
        public virtual Amenity Amenity { get; set; }
    }
}
