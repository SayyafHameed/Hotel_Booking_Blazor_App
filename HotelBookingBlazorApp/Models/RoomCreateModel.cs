using System.ComponentModel.DataAnnotations;

namespace HotelBookingBlazorApp.Models
{
    public class RoomTypeCreateModel
    {
        public short Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Image { get; set; }

        [Required, Range(1, double.MaxValue)]
        public decimal Price { get; set; }

        [Required, MaxLength(200)]
        public string Description { get; set; }

        [Range(1, 10)]
        public int MaxAdults { get; set; }

        [Range(0, 10)]
        public int MaxChildern { get; set; }

        public bool IsActive { get; set; }

        public RoomTypeAmenityCreateModel[] Amenities { get; set; } = [];

       // public AmenitySelectionModel[] AmenitySelections { get; set; } = [];

        public class RoomTypeAmenityCreateModel
        {
            public int Id { get; set; }

            public int? Unit { get; set; }

            public RoomTypeAmenityCreateModel(int id, int? unit) => (Id, Unit) = (id,unit);
        }

        

    }

    
}
