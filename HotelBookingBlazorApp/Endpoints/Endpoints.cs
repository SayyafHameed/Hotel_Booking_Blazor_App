using HotelBookingBlazorApp.Constants;
using HotelBookingBlazorApp.Services;

namespace HotelBookingBlazorApp.Endpoints
{
    public static class Endpoints
    {
        public static IEndpointRouteBuilder MapCustomEndpoints(this IEndpointRouteBuilder builder)
        {
            var staffAdmingroup = builder.MapGroup("/staff-admin")
                .RequireAuthorization(authPolicyBuilder => authPolicyBuilder.RequireRole(RoleType.Admin.ToString(), RoleType.Staff.ToString()));

            staffAdmingroup.MapPost("/manage-amenities/delete/{amenityId:int}",
                async (int amenityId, IAmenitiesService amenityService) =>
                {
                    await amenityService.DeleteAmenityAsync(amenityId);
                    return TypedResults.LocalRedirect("~/staff-admin/manage-amenities");
                });
            return builder;
        }
    }
}
