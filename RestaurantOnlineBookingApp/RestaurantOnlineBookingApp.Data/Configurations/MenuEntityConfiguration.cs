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
    public class MenuEntityConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasData(this.UploadMenus());
        }

        private Menu[] UploadMenus()
        {
            ICollection<Menu> menus = new HashSet<Menu>();

            Menu menu;

            menu = new Menu()
            {
                Id = 1,
                Name = "Menu1",
                Description = "Try our delicious food",

            };
            menus.Add(menu);

            menu = new Menu()
            {
                Id = 2,
                Name = "Menu2",
                Description = "Try our delicious food",
            };
            menus.Add(menu);

            return menus.ToArray();
        }
    }
}