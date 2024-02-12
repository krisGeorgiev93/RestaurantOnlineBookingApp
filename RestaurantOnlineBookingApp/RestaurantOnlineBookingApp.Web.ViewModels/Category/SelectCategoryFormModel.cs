using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Web.ViewModels.Category
{
    public class SelectCategoryFormModel
    {
        //receive this info from DB and initialize it in the form
        public int Id { get; set; }

        public string Name { get; set; } = null!;


    }
}
