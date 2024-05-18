using HotelBookingBlazorApp.Data;
using HotelBookingBlazorApp.Data.Entities;
using HotelBookingBlazorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingBlazorApp.Services
{
    public interface IRoomTypeService
    {
        Task<MethodResult<short>> CreateRoomTypeAsync(RoomTypeCreateModel roomCreateModel, string userId);
    }

    public class RoomTypeService : IRoomTypeService
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public RoomTypeService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<MethodResult<short>> CreateRoomTypeAsync(RoomTypeCreateModel roomCreateModel, string userId)
        {
            using var context = this.contextFactory.CreateDbContext();
            if (await context.RoomTypes.AnyAsync(rt => rt.Name == roomCreateModel.Name))
            {
                return $"Room Type with the same name {roomCreateModel.Name} already exists";
            }

            var roomType = new RoomType
            {
                Name = roomCreateModel.Name,
                AddedBy = userId,
                AddedOn = DateTime.Now,
                Description = roomCreateModel.Description,
                Image = roomCreateModel.Image,
                IsActive = roomCreateModel.IsActive,
                MaxAdults = roomCreateModel.MaxAdults,
                MaxChildern = roomCreateModel.MaxChildern,
                Price = roomCreateModel.Price
            };

            await context.RoomTypes.AddAsync(roomType);
            await context.SaveChangesAsync();

            if (roomCreateModel.Amenities.Length > 0)
            {
                var roomTypeAmenities = roomCreateModel.Amenities.Select(a => new RoomTypeAmenity
                {
                    AmenityId = a.Id,
                    RoomTypeId = a.Id,
                    Unit = a.Unit
                });

                await context.RoomTypeAmenities.AddRangeAsync(roomTypeAmenities);
                await context.SaveChangesAsync();
            }

            return roomType.Id;
        }
    }
}
