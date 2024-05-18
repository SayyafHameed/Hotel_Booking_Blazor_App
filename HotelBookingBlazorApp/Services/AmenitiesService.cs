using HotelBookingBlazorApp.Data;
using HotelBookingBlazorApp.Data.Entities;
using HotelBookingBlazorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingBlazorApp.Services
{
    public interface IAmenitiesService
    {
        Task<Amenity[]> GetAmenityAsync();
        Task<MethodResult<Amenity>> SaveAmenityAsync(Amenity amenity);
        Task<MethodResult<bool>> DeleteAmenityAsync(int id);
    }

    public class AmenitiesService : IAmenitiesService
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public AmenitiesService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<Amenity[]> GetAmenityAsync()
        {
            using var context = this.contextFactory.CreateDbContext();
            return await context.Amenities.Where(a => !a.IsDeleted).ToArrayAsync();
        }

        public async Task<MethodResult<bool>> DeleteAmenityAsync(int id)
        {
            using var context = this.contextFactory.CreateDbContext();
            var amenity = await context.Amenities.AsTracking().FirstOrDefaultAsync(a => a.Id == id);
            if (amenity is not null)
            {
                amenity.IsDeleted = true;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<MethodResult<Amenity>> SaveAmenityAsync(Amenity amenity)
        {
            using var context = this.contextFactory.CreateDbContext();
            if (amenity.Id == 0)
            {
                // create new amenity

                if (await context.Amenities.AnyAsync(a => a.Name == amenity.Name))
                {
                   // return MethodResult<Amenity>.Failure("Amenity exists already");
                    return "Amenity with the same name already exists";
                }
                await context.Amenities.AddAsync(amenity);
            }
            else
            {
                // update existing amenity

                if (await context.Amenities.AnyAsync(a => a.Name == amenity.Name && a.Id != amenity.Id))
                {
                    return "Amenity with the same name already exists";
                }

                var dbAmenity = await context.Amenities.AsTracking().FirstOrDefaultAsync(a => a.Id == amenity.Id) ?? throw new InvalidOperationException("Amenity dose not exist");

                dbAmenity.Name = amenity.Name;
                dbAmenity.Icon = amenity.Icon;

            }
            await context.SaveChangesAsync();
            return amenity;
        }
    }
}
