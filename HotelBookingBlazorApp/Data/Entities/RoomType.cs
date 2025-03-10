﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingBlazorApp.Data.Entities
{
    public class RoomType
    {
        public short Id { get; set; }

        [Required, MaxLength(100), Unicode(false)]
        public string Name { get; set; }

        [Required, MaxLength(100), Unicode(false)]
        public string Image { get; set; }

        [Required, Range(1, double.MaxValue)]
        public decimal Price { get; set; }

        [Required, MaxLength(200)]
        public string Description { get; set; }

        public int MaxAdults { get; set; }
        public int MaxChildern { get; set; }

        public bool IsActive { get; set; }

        public DateTime AddedOn { get; set; }

        [Required]
        public string AddedBy { get; set; }

        public DateTime? LastUpdateOn { get; set; }
        public string? LastUpdatedBy { get; set; }

        [ForeignKey(nameof(AddedBy))]
        public ApplicationUser AddedByUser { get; set; }

        public virtual ICollection<RoomTypeAmenity> Amenities { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
