using RestaurantOnlineBookingApp.Data.Models;

namespace RestaurantOnlineBookingApp.Data.Configurations
{
    public class CapacityPerDateSeeder
    {
        public static List<CapacityPerDate> SeedCapacities(List<Restaurant> restaurants)
        {
            var capacities = new List<CapacityPerDate>();

            foreach (var restaurant in restaurants)
            {
                int capacityIdCounter = 1;

                // капацитети за следващите 60 дни за всеки ресторант
                for (int i = 0; i < 60; i++)
                {
                    var date = DateTime.Now.Date.AddDays(i);
                    var capacity = new CapacityPerDate
                    {
                        Id = capacityIdCounter++,
                        RestaurantId = restaurant.Id,
                        Date = date,
                        Capacity = restaurant.Capacity
                    };

                    capacities.Add(capacity);
                }
            }

            return capacities;
        }
    }
}