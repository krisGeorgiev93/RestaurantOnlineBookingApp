using RestaurantOnlineBookingApp.Web.ViewModels.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface IReviewService
    {
        Task AddReviewAsync(AddReviewViewModel model);
    }
}
