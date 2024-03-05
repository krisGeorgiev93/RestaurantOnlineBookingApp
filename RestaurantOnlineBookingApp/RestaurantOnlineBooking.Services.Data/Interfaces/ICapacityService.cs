namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface ICapacityService
    {
        Task AddCapacitiesFor60DaysAsync(Guid restaurantId, int capacity, string startDateString);


    }
}
