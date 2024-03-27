using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Owner = RestaurantOnlineBookingApp.Data.Models.Owner;

namespace RestaurantOnlineBookingApp.Data.Configurations
{
    public class OwnerEntityConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasData(new Owner()
            {
                Id = Guid.Parse("44e7b2ef-dfa8-45ae-aca9-0b52b9a3df4d"),
                PhoneNumber = "+359888888888",
                UserId = Guid.Parse("faf6dc41-ce01-44a9-b63c-0abd2df2d15f")             

            });
        }
    }
}
