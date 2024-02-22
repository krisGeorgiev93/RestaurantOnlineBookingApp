using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOnlineBookingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Data.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(this.UploadCategories());
        }

        private Category[] UploadCategories()
        {
            ICollection<Category> categories = new HashSet<Category>();

            Category category;

            category = new Category()
            {
                Id = 1,
                Name = "Family"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Buffet"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Coffee house"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 4,
                Name = "Mediterranean"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 5,
                Name = "Desert House"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 6,
                Name = "Chinese"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 7,
                Name = "Indian"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 8,
                Name = "Lebanese"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
